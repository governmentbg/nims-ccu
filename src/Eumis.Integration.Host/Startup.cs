using Autofac;
using Autofac.Integration.WebApi;
using Eumis.ApplicationServices;
using Eumis.Authentication;
using Eumis.Authentication.Owin;
using Eumis.Common.Api;
using Eumis.Common.Config;
using Eumis.Common.Owin;
using Eumis.Data;
using Eumis.Data.Core.Interception;
using Eumis.Domain;
using Eumis.Integration.Api;
using Eumis.Log;
using Eumis.Log.Api;
using Eumis.Log.NLog;
using Eumis.Log.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Eumis.Integration.Host
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

        public static readonly ILoggerFactory LoggerFactory = new NLogLoggerFactory(NLogLogger.IntegrationLoggerName);

        public static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AuthenticationModule());
            builder.RegisterModule(new ApplicationServicesModule());
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new IntegrationApiModule());
            builder.RegisterModule(new LogModule(LoggerFactory));
            builder.RegisterModule(new IntegrationHostModule());
            return builder.Build();
        }

        public static void Configure(IAppBuilder app, IContainer container)
        {
            string logs1ConnectionString = ConfigurationManager.ConnectionStrings["Logs1"].ConnectionString.ExpandEnv();
            LogManager.Configuration.SetDatabaseTargetConnectionString("_databaseLog", logs1ConnectionString);
            LogManager.Configuration.SetDatabaseTargetConnectionString("_requestDbLog", logs1ConnectionString);
            LogManager.Configuration.SetDatabaseTargetConnectionString("_responseDbLog", logs1ConnectionString);

            app.UseAutofacMiddleware(container);
            app.UseEumisOAuthBearerAuthentication();
            app.UseLogging(LoggerFactory);

            ConfigureWebApi(app);
            DbInterception.Add(new BridgingDbCommandInterceptor());
            app.DisposeContainerOnShutdown(container);
        }

        public static void ConfigureWebApi(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // auth
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new AuthorizeAttribute());

            // json serialization
            config.Formatters.JsonFormatter.SerializerSettings = JsonSerializerSettings;
            JsonConvert.DefaultSettings = () => JsonSerializerSettings;

            // logging
            config.Services.Add(typeof(IExceptionLogger), new LoggingMiddlewareExceptionLogger(
                new Type[]
                {
                    typeof(DataUpdateConcurrencyException),
                    typeof(DomainObjectNotFoundException),
                    typeof(DataObjectNotFoundException),
                }));
            config.MessageHandlers.Add(new LoggingMessageHandler());

            // remove all services of type IFilterProvider
            config.Services.RemoveAll(typeof(IFilterProvider), t => true);

            config.Services.Add(typeof(IFilterProvider), new ConfigurationFilterProvider());
            config.Services.Add(typeof(IFilterProvider), new OrderedActionDescriptorFilterProvider());

            // routing
            config.MapHttpAttributeRoutes(new InheritDirectRouteProvider());

            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
            app.Use(new Func<AppFunc, AppFunc>(next => async (env) =>
            {
                env["owin.ResponseStatusCode"] = 200;

                ((IDictionary<string, string[]>)env["owin.ResponseHeaders"])["Content-Type"] = new[] { "text/plain" };

                using (StreamWriter sw = new StreamWriter((Stream)env["owin.ResponseBody"]))
                {
                    await sw.WriteAsync($"Integrations -> Eumis running at {DateTime.Now}");
                }
            }));
        }

        public void Configuration(IAppBuilder app)
        {
            var container = CreateAutofacContainer();

            Configure(app, container);
        }
    }
}
