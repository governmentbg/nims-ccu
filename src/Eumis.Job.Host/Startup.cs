using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Threading;
using Autofac;
using Eumis.Common.Config;
using Eumis.Common.Owin;
using Eumis.Data;
using Eumis.Data.Core.Interception;
using Eumis.Domain;
using Eumis.Job.Host.Core;
using Eumis.Job.Host.Jobs.Email;
using Eumis.Job.Host.Jobs.InternalNotification;
using Eumis.Job.Host.Jobs.Procedures;
using Eumis.Job.Host.Jobs.Projects;
using Eumis.Log;
using Eumis.Log.NLog;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog;
using Owin;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Eumis.Job.Host
{
    public class Startup
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
#if DEBUG
            Formatting = Formatting.Indented,
#else
            Formatting = Formatting.None,
#endif
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Include,
            DefaultValueHandling = DefaultValueHandling.Include,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>()
            {
                new StringEnumConverter { CamelCaseText = true },
            },
        };

        public static readonly ILoggerFactory LoggerFactory = new NLogLoggerFactory("Eumis.Job.Host");

        public static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new DataModule());

            builder.Register(c => LoggerFactory.Create());
            builder.RegisterType<JobHost>();
            builder.RegisterType<EmailJob>().As<IJob>();
            builder.RegisterType<ProcedureChangeStatusJob>().As<IJob>();
            builder.RegisterType<ProjectCommunicationStatusJob>().As<IJob>();
            builder.RegisterType<ProjectQuestionExpireJob>().As<IJob>();
            builder.RegisterType<NotificationDistributorJob>().As<IJob>();

            return builder.Build();
        }

        public static void Configure(IAppBuilder app, IContainer container)
        {
            string logs1ConnectionString = ConfigurationManager.ConnectionStrings["Logs1"].ConnectionString.ExpandEnv();
            LogManager.Configuration.SetDatabaseTargetConnectionString("_databaseLog", logs1ConnectionString);

            // return 200 OK / "Running" on all requests
            app.Use(new Func<AppFunc, AppFunc>(next => async (env) =>
            {
                env["owin.ResponseStatusCode"] = 200;

                ((IDictionary<string, string[]>)env["owin.ResponseHeaders"])["Content-Type"] = new[] { "text/plain" };

                using (StreamWriter sw = new StreamWriter((Stream)env["owin.ResponseBody"]))
                {
                    await sw.WriteAsync("Running");
                }
            }));

            StartJobs(container, app);
            DbInterception.Add(new BridgingDbCommandInterceptor());
            app.DisposeContainerOnShutdown(container);
        }

        public static void StartJobs(IContainer container, IAppBuilder app)
        {
            var context = new OwinContext(app.Properties);
            var token = context.Get<CancellationToken>("host.OnAppDisposing");

            var jobs = container.Resolve<IJob[]>();

            foreach (var job in jobs)
            {
                if (job.Period != TimeSpan.Zero)
                {
                    container.Resolve<JobHost>().Start(job, token);
                }
            }
        }

        public void Configuration(IAppBuilder app)
        {
            var container = CreateAutofacContainer();

            Configure(app, container);
        }
    }
}
