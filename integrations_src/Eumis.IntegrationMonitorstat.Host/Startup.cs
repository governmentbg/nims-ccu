using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Extras.NLog;
using Autofac.Integration.WebApi;
using Eumis.Common;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Config;
using Eumis.Common.Log;
using Eumis.Common.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Monitorstat.Common;
using Newtonsoft.Json;
using Owin;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Eumis.Integration.Monitorstat
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
        };

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            var builder = new ContainerBuilder();

            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new MonitorstatCommonModule());
            builder.RegisterModule(new MonitorstatModule());
            builder.RegisterModule<NLogModule>();
            builder.RegisterHttpRequestMessage(config);

            var container = builder.Build();

            // replace default webapi error handler
            config.Services.Replace(typeof(IExceptionHandler), new WebApiExceptionHandler());

            app.UseRequestLoggingMiddleware();
            app.UseLoggingMiddleware();

            // json serialization
            config.Formatters.JsonFormatter.SerializerSettings = JsonSerializerSettings;
            JsonConvert.DefaultSettings = () => JsonSerializerSettings;

            // routing
            config.MapHttpAttributeRoutes(new InheritDirectRouteProvider());

            app.UseCors(CorsOptions.AllowAll);

            var oauthAuthorizationServerOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/token"),
                Provider = new ApplicationOAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:OAuthTokenExpirationMinutes"))),
                AllowInsecureHttp = bool.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:OAuthAllowInsecureHttp")),
                AccessTokenFormat = new TicketDataFormat(
                    new AesDataProtector(
                        ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:InstallationName"),
                        ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:Key"),
                        ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:Preamble"))),
            };

            // set the OAuthAuthorizationServerOptions in every request environment
            app.Use((ctx, next) =>
            {
                ctx.Set<OAuthAuthorizationServerOptions>("eumis.OAuthAuthorizationServerOptions", oauthAuthorizationServerOptions);

                return next();
            });

            // return json to browser GET request
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            this.ConfigureAuth(app, oauthAuthorizationServerOptions);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
            config.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
            app.DisposeContainerOnShutdown(container);

            // return 200 OK / "Running" on all requests
            app.Use(new Func<AppFunc, AppFunc>(next => async (env) =>
            {
                env["owin.ResponseStatusCode"] = 200;

                ((IDictionary<string, string[]>)env["owin.ResponseHeaders"])["Content-Type"] = new[] { "text/plain" };

                using (StreamWriter sw = new StreamWriter((Stream)env["owin.ResponseBody"]))
                {
                    await sw.WriteAsync($"Eumis -> Monitorstat running at {DateTime.Now}");
                }
            }));
        }

        public void ConfigureAuth(IAppBuilder app, OAuthAuthorizationServerOptions oauthAuthorizationServerOptions)
        {
            if (bool.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.IntegrationMonitorstat.Host:EnableOAuthAuthorizationServer")))
            {
                app.UseOAuthAuthorizationServer(oauthAuthorizationServerOptions);
            }

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                AccessTokenFormat = oauthAuthorizationServerOptions.AccessTokenFormat,
                Provider = new QueryStringOAuthBearerProvider(),

                // override the token deserialization to be able to capture the properties
                AccessTokenProvider = new AuthenticationTokenProvider()
                {
                    OnReceive = (c) =>
                    {
                        c.DeserializeTicket(c.Token);

                        // check if invalid bearer token received
                        if (c.Ticket != null)
                        {
                            c.OwinContext.SetAuthenticationProperties(c.Ticket.Properties.Dictionary);
                        }
                    },
                },
            });
        }
    }
}
