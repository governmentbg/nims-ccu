using Autofac;
using Autofac.Integration.WebApi;
using Eumis.Blob.Host.Api;
using Eumis.Blob.Host.Auth;
using Eumis.Common.Config;
using Eumis.Common.Owin;
using Eumis.Log;
using Eumis.Log.Api;
using Eumis.Log.NLog;
using Eumis.Log.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using NLog;
using Owin;
using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Eumis.Blob.Host
{
    public class Startup
    {
        private ILoggerFactory loggerFactory = new NLogLoggerFactory("Eumis.Blob.Host");

        public void Configuration(IAppBuilder app)
        {
            var container = this.CreateAutofacContainer();

            this.Configure(app, container);
        }

        public IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BlobHostModule());
            builder.RegisterModule(new LogModule(this.loggerFactory));
            return builder.Build();
        }

        public void Configure(IAppBuilder app, IContainer container)
        {
            string logs1ConnectionString = ConfigurationManager.ConnectionStrings["Logs1"].ConnectionString.ExpandEnv();
            LogManager.Configuration.SetDatabaseTargetConnectionString("_databaseLog", logs1ConnectionString);

            var oauthAuthorizationServerOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/token"),
                Provider = new ApplicationOAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Blob.Host:OAuthTokenExpirationMinutes"))),
                AllowInsecureHttp = bool.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Blob.Host:OAuthAllowInsecureHttp")),
                AccessTokenFormat = new TicketDataFormat(
                    app.CreateDataProtector(
                        "Microsoft.Owin.Security.OAuth",
                        "Authentication_Code",
                        "v1")),
            };

            // set the OAuthAuthorizationServerOptions in every request environment
            app.Use((ctx, next) =>
            {
                ctx.Set<OAuthAuthorizationServerOptions>("eumis.OAuthAuthorizationServerOptions", oauthAuthorizationServerOptions);

                return next();
            });

            app.UseAutofacMiddleware(container);
            app.UseLogging(this.loggerFactory);
            this.ConfigureAuth(app, oauthAuthorizationServerOptions);
            this.ConfigureWebApi(app, oauthAuthorizationServerOptions);
            app.DisposeContainerOnShutdown(container);
        }

        public void ConfigureAuth(IAppBuilder app, OAuthAuthorizationServerOptions oauthAuthorizationServerOptions)
        {
            app.UseOAuthAuthorizationServer(oauthAuthorizationServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
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

        public void ConfigureWebApi(IAppBuilder app, OAuthAuthorizationServerOptions oauthAuthorizationServerOptions)
        {
            HttpConfiguration config = new HttpConfiguration();

            // auth
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(oauthAuthorizationServerOptions.AuthenticationType));
            config.Filters.Add(new AuthorizeAttribute());

            // logging
            config.Services.Add(typeof(IExceptionLogger), new LoggingMiddlewareExceptionLogger());

            // routing
            config.MapHttpAttributeRoutes();

            // fix ie9 not supporting json
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            app.UseCors(new CorsOptions { PolicyProvider = new EumisCorsPolicyProvider() });
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}
