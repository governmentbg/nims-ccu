using Autofac.Features.OwnedInstances;
using Eumis.Common;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Common.Log;
using Eumis.Data.Emails.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Job.Host.Core;
using Newtonsoft.Json.Linq;
using RazorEngine.Templating;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Job.Host.Jobs.Email
{
    public class EmailJob : IJob
    {
        private Func<Owned<DisposableTuple<IUnitOfWork, IEmailsRepository>>> dependencyFactory;
        private object syncRoot = new object();
        private ILogger logger;
        private ITemplateService templateService;
        private ConcurrentDictionary<string, string> compiledTemplates;
        private bool disposed;
        private int batchSize;
        private TimeSpan period;
        private int maxFailedAttempts;
        private TimeSpan failedAttemptTimeout;
        private int parallelMailTasks;
        private string mailServer;
        private string mailServerUsername;
        private string mailServerPassword;
        private string mailServerDomain;
        private string devOverrideEmail;
        private int successes;
        private int failures;

        public EmailJob(Func<Owned<DisposableTuple<IUnitOfWork, IEmailsRepository>>> dependencyFactory, ILogger logger)
        {
            this.dependencyFactory = dependencyFactory;
            this.logger = logger;
            this.templateService = new TemplateService();
            this.compiledTemplates = new ConcurrentDictionary<string, string>();
            this.disposed = false;

            this.batchSize = int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:EmailJobBatchSize"));
            this.period = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:EmailJobPeriodInSeconds")));
            this.maxFailedAttempts = int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:EmailJobMaxFailedAttempts"));
            this.failedAttemptTimeout = TimeSpan.FromMinutes(double.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:EmailJobFailedAttemptTimeoutInMinutes")));
            this.parallelMailTasks = int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:EmailJobParallelMailTasks"));
            this.mailServer = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:MailServer");
            this.mailServerUsername = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:MailServerUsername");
            this.mailServerPassword = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:MailServerPassword");
            this.mailServerDomain = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:MailServerDomain");
            this.devOverrideEmail = ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:DevOverrideEmail");
        }

        public string Name
        {
            get { return "EmailJob"; }
        }

        public TimeSpan Period
        {
            get { return this.period; }
        }

        public void Action(CancellationToken ct)
        {
            IList<int> pendingEmailIds = new List<int>();

            try
            {
                if (this.disposed)
                {
                    return;
                }

                using (var factory = this.dependencyFactory())
                {
                    var unitOfWork = factory.Value.Item1;
                    var emailsRepository = factory.Value.Item2;

                    pendingEmailIds = emailsRepository.GetPendingEmailIds(this.batchSize, this.maxFailedAttempts, this.failedAttemptTimeout);
                }

                if (pendingEmailIds.Any())
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    this.successes = 0;
                    this.failures = 0;

                    this.SendParallel(pendingEmailIds, ct).Wait();

                    sw.Stop();
                    this.logger.Log(
                        LogLevel.Info,
                        string.Format("Email batch finished in {0}ms - {1} emails send, {2} failures of total {3} emails.", sw.ElapsedMilliseconds, this.successes, this.failures, pendingEmailIds.Count));
                }
            }
            catch (OperationCanceledException ex)
            {
                this.logger.Log(
                    LogLevel.Error,
                    string.Format("Job was canceled due to a token cancellation request; Email batch finished with {0} emails send, {1} failures of total {2} emails.", this.successes, this.failures, pendingEmailIds.Count),
                    ex);
            }
            catch (Exception ex)
            {
                this.logger.Log(LogLevel.Error, ex.Message, ex);
            }
        }

        private Task SendParallel(IList<int> pendingEmailIds, CancellationToken ct)
        {
            ConcurrentQueue<int> mailIds = new ConcurrentQueue<int>(pendingEmailIds);

            int numberOfParallelTasks = Math.Min(mailIds.Count, this.parallelMailTasks);
            var parallelTasks = Enumerable.Range(0, numberOfParallelTasks)
                .Select(pt => Task.Run(() => this.Send(mailIds, ct), ct))
                .ToArray();

            return Task.WhenAll(parallelTasks);
        }

        private async Task Send(ConcurrentQueue<int> mailIds, CancellationToken ct)
        {
            Eumis.Domain.Emails.Email email;

            using (SmtpClient smtpClient = new SmtpClient(this.mailServer))
            {
                if (!string.IsNullOrEmpty(this.mailServerUsername))
                {
                    var credentials = new NetworkCredential(this.mailServerUsername, this.mailServerPassword);

                    if (!string.IsNullOrEmpty(this.mailServerDomain))
                    {
                        credentials.Domain = this.mailServerDomain;
                    }

                    smtpClient.Credentials = credentials;
                }

                using (var factory = this.dependencyFactory())
                {
                    var unitOfWork = factory.Value.Item1;
                    var emailsRepository = factory.Value.Item2;

                    while (mailIds.TryDequeue(out int mailId))
                    {
                        if (this.disposed)
                        {
                            break;
                        }

                        ct.ThrowIfCancellationRequested();

                        email = emailsRepository.Find(mailId);

                        try
                        {
                            TemplateConfig templateConfig = TemplateConfig.Get(email.MailTemplateName);

                            MailAddress from = new MailAddress(templateConfig.Sender);
                            MailAddress to = new MailAddress(string.IsNullOrEmpty(this.devOverrideEmail) ? email.Recipient : this.devOverrideEmail);

                            MailMessage mailMessage = new MailMessage(from, to)
                            {
                                Subject = templateConfig.MailSubject,
                                Body = this.BuildEmailBody(templateConfig.TemplateFileName, email.Context),
                                SubjectEncoding = System.Text.Encoding.GetEncoding(1251),
                                BodyEncoding = System.Text.Encoding.UTF8,
                                IsBodyHtml = templateConfig.IsBodyHtml,
                                DeliveryNotificationOptions = DeliveryNotificationOptions.None,
                            };

                            await smtpClient.SendMailAsync(mailMessage);

                            email.SetStatus(EmailStatus.Sent);
                            email.ModifyDate = DateTime.Now;
                            Interlocked.Increment(ref this.successes);
                        }
                        catch (SmtpException smtpEx)
                        {
                            var exception = string.Format("SmtpException: code->{0}, message->{1}, source->{2}, stacktrace->{3}", Enum.GetName(typeof(SmtpStatusCode), smtpEx.StatusCode), smtpEx.Message, smtpEx.Source, smtpEx.StackTrace);
                            email.IncrementFailedAttempts(exception);
                            this.logger.Log(LogLevel.Warn, exception);
                            Interlocked.Increment(ref this.failures);
                        }
                        catch (Exception ex)
                        {
                            email.SetStatus(EmailStatus.UknownError);
                            email.IncrementFailedAttempts(ex.Message);
                            this.logger.Log(LogLevel.Error, ex.Message, ex);
                            Interlocked.Increment(ref this.failures);
                        }

                        unitOfWork.Save();
                    }
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.templateService.Dispose();
                this.templateService = null;

                this.disposed = true;

                this.logger.Log(LogLevel.Info, "Email job disposed");
            }
        }

        private string BuildEmailBody(string templateFileName, string context)
        {
            if (!this.compiledTemplates.ContainsKey(templateFileName))
            {
                lock (this.syncRoot)
                {
                    if (!this.compiledTemplates.ContainsKey(templateFileName))
                    {
                        string templatePath = this.GetTemplatePath(templateFileName);
                        string razorTemplate = File.ReadAllText(templatePath);
                        this.templateService.Compile(razorTemplate, typeof(JObject), templateFileName);
                        this.compiledTemplates.TryAdd(templateFileName, templateFileName);
                    }
                }
            }

            return this.templateService.Run(templateFileName, context != null ? JObject.Parse(context) : null, null);
        }

        private string GetTemplatePath(string templateName)
        {
            string assemblyPath = new System.Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            string binPath = System.IO.Path.GetDirectoryName(assemblyPath);
            string templateFullPath = string.Format(@"{0}\Jobs\Email\Templates\{1}", binPath, templateName);

            return templateFullPath;
        }
    }
}
