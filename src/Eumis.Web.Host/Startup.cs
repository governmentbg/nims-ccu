using Autofac;
using Autofac.Integration.WebApi;
using Eumis.ApplicationServices;
using Eumis.Authentication;
using Eumis.Authentication.Owin;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Config;
using Eumis.Common.Owin;
using Eumis.Data;
using Eumis.Data.Core.Interception;
using Eumis.Domain;
using Eumis.Log;
using Eumis.Log.Api;
using Eumis.Log.NLog;
using Eumis.Log.Owin;
using Eumis.Print;
using Eumis.Web.Api;
using Eumis.Web.Host.Api;
using Eumis.Web.Host.Nancy;
using Eumis.Web.Host.Owin;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Nancy.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Eumis.Web.Host
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

        public static readonly ILoggerFactory LoggerFactory = new NLogLoggerFactory("Eumis.Web.Host", new Dictionary<string, Func<IOwinContext, string>>()
        {
            { "SessionId", (owinContext) => owinContext.Request.Environment[SessionMiddleware.SessionIdOwinEnvKey] as string },
        });

        public static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AuthenticationModule());
            builder.RegisterModule(new ApplicationServicesModule());
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new WebApiModule());
            builder.RegisterModule(new PrintModule());
            builder.RegisterModule(new LogModule(LoggerFactory));
            builder.RegisterModule(new WebHostModule());
            return builder.Build();
        }

        public static void Configure(IAppBuilder app, IContainer container)
        {
            string logs1ConnectionString = ConfigurationManager.ConnectionStrings["Logs1"].ConnectionString.ExpandEnv();
            LogManager.Configuration.SetDatabaseTargetConnectionString("_databaseLog", logs1ConnectionString);
            LogManager.Configuration.SetDatabaseTargetConnectionString("_requestDbLog", logs1ConnectionString);
            LogManager.Configuration.SetDatabaseTargetConnectionString("_responseDbLog", logs1ConnectionString);

            app.UseAutofacMiddleware(container);
            app.UseSession();
            app.UseEumisOAuthAuthorizationServer();
            app.UseEumisCookieAuthentication();
            app.UseEumisOAuthBearerAuthentication();

            // exclude static files from logging
            // by placing them before UseLogging
            ConfigureUnsecuredStaticFiles(app);

            app.UseLogging(LoggerFactory);

            app.UseGlobalization();

            // nancy app branch
            app.MapWhen(
                owinCtx =>
                    //// HomeModule
                    owinCtx.Request.Path == new PathString("/") ||
                    owinCtx.Request.Path == new PathString("/gdprdeclaration") ||
                    owinCtx.Request.Path == new PathString("/gdprdeclarationView") ||
                    owinCtx.Request.Path == new PathString("/declaration") ||

                    // LoginModule
                    owinCtx.Request.Path == new PathString("/login") ||
                    owinCtx.Request.Path == new PathString("/logout") ||

                    // BrowserNotSupportedModule
                    owinCtx.Request.Path == new PathString("/browserNotSupported") ||

                    // NewPasswordModule
                    owinCtx.Request.Path == new PathString("/newPassword") ||

                    // RecoverPasswordModule
                    owinCtx.Request.Path == new PathString("/recoverPassword"),
                branch =>
                {
                    // handle specific exceptions from the NancyMiddleware
                    branch.Use(new Func<AppFunc, AppFunc>(next => async (env) =>
                    {
                        try
                        {
                            // "next" should always be the NancyMiddleware
                            await next(env);
                        }
                        catch (HttpException ex)
                        {
                            // swallow communication exceptions, we can't do anything about them
                            if (!ex.Message.StartsWith("The remote host closed the connection."))
                            {
                                throw;
                            }
                        }
                    }));

                    // terminate all nancy requests with the nancy middleware
                    branch.UseNancy();
                });

            app.UseWhen(
                owinCtx => owinCtx.Request.Path.StartsWithSegments(new PathString("/api/authentication")),
                branch =>
                {
                    branch.SetUserAuthenticationCookie(AuthenticationTypes.Bearer);
                });

            app.UseWhen(
                owinCtx => !owinCtx.Request.Path.StartsWithSegments(new PathString("/api/historicContracts")),
                branch =>
                {
                    branch.RequireUserAuthentication(AuthenticationTypes.Cookie);
                    ConfigureSecuredStaticFiles(branch);
                });

            ConfigureWebApi(app);

            DbInterception.Add(new BridgingDbCommandInterceptor());
            app.DisposeContainerOnShutdown(container);
        }

        public static void ConfigureWebApi(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // auth
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(AuthenticationTypes.Bearer));
            config.Filters.Add(new AuthorizeAttribute());

            // json serialization
            config.Formatters.JsonFormatter.SerializerSettings = JsonSerializerSettings;
            JsonConvert.DefaultSettings = () => JsonSerializerSettings;

            // custom filters
            config.Filters.Add(new DataUpdateConcurrencyExceptionFilterAttribute());
            config.Filters.Add(new ObjectNotFoundExceptionFilterAttribute());
            config.Filters.Add(new NoCacheActionFilterAttribute());
            config.Filters.Add(new HistoricContractImportExceptionFilterAttribute());

            // logging
            config.Services.Add(typeof(IExceptionLogger), new LoggingMiddlewareExceptionLogger(
                new Type[]
                {
                    typeof(DataUpdateConcurrencyException),
                    typeof(DomainObjectNotFoundException),
                    typeof(DataObjectNotFoundException),
                    typeof(HistoricContractImportException),
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
        }

        public static void ConfigureUnsecuredStaticFiles(IAppBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString("/css"),
                FileSystem = new PhysicalFileSystem("./App/css"),
                ContentTypeProvider = new ContentTypeProvider(),
                ServeUnknownFileTypes = false,
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString("/fonts"),
                FileSystem = new PhysicalFileSystem("./App/fonts"),
                ContentTypeProvider = new ContentTypeProvider(),
                ServeUnknownFileTypes = false,
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString("/img"),
                FileSystem = new PhysicalFileSystem("./App/img"),
                ContentTypeProvider = new ContentTypeProvider(),
                ServeUnknownFileTypes = false,
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString("/templates"),
                FileSystem = new PhysicalFileSystem("./App/templates"),
                ContentTypeProvider = new ContentTypeProvider(),
                ServeUnknownFileTypes = true,
            });
        }

        public static void ConfigureSecuredStaticFiles(IAppBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString("/js"),
                FileSystem = new PhysicalFileSystem("./App/js"),
                ContentTypeProvider = new ContentTypeProvider(),
                ServeUnknownFileTypes = false,
            });
        }

        public void Configuration(IAppBuilder app)
        {
            var container = CreateAutofacContainer();

            Configure(app, container);
        }
    }
}
