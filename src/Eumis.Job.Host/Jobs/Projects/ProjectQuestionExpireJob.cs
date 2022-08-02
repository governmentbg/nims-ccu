using Autofac.Features.OwnedInstances;
using Eumis.Common;
using Eumis.Common.Config;
using Eumis.Common.Db;
using Eumis.Common.Email;
using Eumis.Common.Log;
using Eumis.Data.Emails.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Job.Host.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Eumis.Job.Host.Jobs.Projects
{
    public sealed class ProjectQuestionExpireJob : IJob
    {
        private readonly Func<Owned<DisposableTuple<IUnitOfWork, IProjectCommunicationsRepository, IEmailsRepository>>> dependencyFactory;
        private readonly ILogger logger;

        public ProjectQuestionExpireJob(Func<Owned<DisposableTuple<IUnitOfWork, IProjectCommunicationsRepository, IEmailsRepository>>> dependencyFactory, ILogger logger)
        {
            this.dependencyFactory = dependencyFactory;
            this.logger = logger;
            this.Period = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Job.Host:ProjectCommunicationStatusJobPeriodInSeconds")));
        }

        public string Name => "ProjectQuestionExpireJob";

        public TimeSpan Period
        {
            get;
            private set;
        }

        public void Action(CancellationToken token)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                using (var factory = this.dependencyFactory())
                {
                    IUnitOfWork unitOfWork = factory.Value.Item1;
                    IProjectCommunicationsRepository projectCommunicationsRepository = factory.Value.Item2;
                    IEmailsRepository emailsRepository = factory.Value.Item3;

                    var communicationsWithTodayEmails = emailsRepository.GetTodayEmailForProjectCommunications();
                    var questionsWithoutAnswer = projectCommunicationsRepository
                        .GetCurrentExpiringQuestions()
                        .Where(s => !communicationsWithTodayEmails.Contains(s.ProjectCommunicationId))
                        .ToList();

                    foreach (var communication in questionsWithoutAnswer)
                    {
                        Domain.Emails.Email email = new Domain.Emails.Email(
                            communication.Recipient,
                            EmailTemplate.ProjectQuestionExpireMessage,
                            JObject.FromObject(communication));

                        emailsRepository.Add(email);
                    }

                    if (questionsWithoutAnswer.Any())
                    {
                        unitOfWork.Save();
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                this.logger.Log(LogLevel.Error, $"{this.Name} was canceled due to a token cancellation request", ex);
            }
            catch (Exception ex)
            {
                this.logger.Log(LogLevel.Error, $"{this.Name} finished with error in {stopwatch.ElapsedMilliseconds} ms", ex);
                throw;
            }

            this.logger.Log(LogLevel.Debug, $"{this.Name} finished in {stopwatch.ElapsedMilliseconds} ms");
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}
