using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Eumis.Common.Helpers;
using Eumis.Common.Jobs;
using Eumis.Components;
using Eumis.Components.ValidationEngine;
using Eumis.Portal.Model;
using System.Web.Mvc;
using Eumis.Portal.Web.Providers;
using Autofac.Integration.WebApi;
using System.Web.Http;

namespace Eumis.Portal.Web.App_Start
{
    public class InitialConfig
    {
        public static void Init()
        {
            // ValidationEngineModelValidatorProvider uses Regexes for the validation and the default of 15 is insufficient
            System.Text.RegularExpressions.Regex.CacheSize = 50;

            //ModelValidatorProviders.Providers.Add(new ValidationEngineModelValidatorProvider());
            //ViewEngines.Engines.Insert(0, new AppContextViewEngine());

            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());

            IContainer container = BuildContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            StartJobs(container);
        }

        public static void StartJobs(IContainer container)
        {
            var jobs = container.Resolve<IJob[]>();

            foreach (var job in jobs)
            {
                (new JobHost(job)).Start();
            }
        }

        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new ConfigurationSettingsReader());

            builder.RegisterModule(new EumisPortalModelModule());
            builder.RegisterModule(new EumisComponentsModule());

            builder.RegisterType(typeof(Controllers.CompaniesController)).InstancePerLifetimeScope();

            builder.RegisterType(typeof(Areas.Private.Controllers.InitializerController)).InstancePerLifetimeScope();
            //builder.RegisterType<MailSenderJob>().As<IJob>().ExternallyOwned();

            return builder.Build();
        }
    }
}