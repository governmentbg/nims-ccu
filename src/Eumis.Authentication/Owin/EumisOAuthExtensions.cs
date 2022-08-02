using Eumis.Authentication.AccessContexts;
using Eumis.Common.Auth;
using Eumis.Common.Config;
using Eumis.Common.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;

namespace Eumis.Authentication.Owin
{
    public static class EumisOAuthExtensions
    {
        private const string EumisCookieAuthName = "authCookie";

        public static IAppBuilder UseEumisOAuthAuthorizationServer(this IAppBuilder app)
        {
            app.SetEumisAuthenticationConfiguration();
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AccessTokenFormat = (TicketDataFormat)app.Properties["eumis.TicketDataFormat"],
                AccessTokenExpireTimeSpan = (TimeSpan)app.Properties["eumis.AuthExpirationTimeSpan"],
                AllowInsecureHttp = (bool)app.Properties["eumis.OAuthAllowInsecureHttp"],
                Provider = new EumisOAuthServerProvider(
                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:Clients"),
                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:ExternalClients")),
                TokenEndpointPath = new PathString("/api/token"),
            });

            return app;
        }

        public static IAppBuilder UseEumisOAuthBearerAuthentication(this IAppBuilder app)
        {
            app.SetEumisAuthenticationConfiguration();
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AuthenticationType = AuthenticationTypes.Bearer,
                Provider = new QueryStringOAuthBearerProvider(),
                AccessTokenProvider = new EumisAuthenticationTokenProvider(),
                AccessTokenFormat = (TicketDataFormat)app.Properties["eumis.TicketDataFormat"],
            });

            return app;
        }

        public static IAppBuilder UseEumisCookieAuthentication(this IAppBuilder app)
        {
            app.SetEumisAuthenticationConfiguration();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                TicketDataFormat = (TicketDataFormat)app.Properties["eumis.TicketDataFormat"],
                AuthenticationType = AuthenticationTypes.Cookie,
                AuthenticationMode = AuthenticationMode.Passive,
                ExpireTimeSpan = (TimeSpan)app.Properties["eumis.AuthExpirationTimeSpan"],
                SlidingExpiration = true,
                CookieName = EumisCookieAuthName,
                CookieHttpOnly = false,
            });

            return app;
        }

        public static IAppBuilder RequireUserAuthentication(this IAppBuilder app, string authenticationType)
        {
            app.Use(async (context, next) =>
            {
                bool authenticated = false;

                if (context.Authentication != null)
                {
                    var authRes = await context.Authentication.AuthenticateAsync(authenticationType);

                    if (authRes != null && authRes.Identity != null && authRes.Identity.IsAuthenticated)
                    {
                        var accessContext = AuthExtensions.CreateAccessContext(authRes);

                        if (accessContext.IsUser)
                        {
                            authenticated = true;
                        }
                    }
                }

                if (authenticated)
                {
                    await next();
                }
                else
                {
                    context.Response.StatusCode = 401;
                }
            });

            return app;
        }

        public static IAppBuilder SetUserAuthenticationCookie(this IAppBuilder app, string authenticationType)
        {
            app.Use(async (context, next) =>
            {
                if (context.Authentication != null)
                {
                    var authRes = await context.Authentication.AuthenticateAsync(authenticationType);

                    if (authRes != null && authRes.Identity != null && authRes.Identity.IsAuthenticated)
                    {
                        var currentToken = context.Get<string>(QueryStringOAuthBearerProvider.AccessTokenKey);

                        if (!string.IsNullOrEmpty(currentToken))
                        {
                            // Valid token passed as URL parameter should be set in cookie and send back
                            context.Response.Cookies.Append(EumisCookieAuthName, currentToken);
                        }

                        context.Response.Redirect("/");
                        return;
                    }
                }

                context.Response.Redirect("/Login");
            });

            return app;
        }

        private static IAppBuilder SetEumisAuthenticationConfiguration(this IAppBuilder app)
        {
            if (app.Properties.ContainsKey("eumis.TicketDataFormat"))
            {
                // already configured
                return app;
            }

            var ticketDataFormat =
                new TicketDataFormat(
                    new AesDataProtector(
                        ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:InstallationName"),
                        ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:Key"),
                        ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:Preamble")));

            app.Properties.Add("eumis.TicketDataFormat", ticketDataFormat);

            var expiresIn = TimeSpan.FromMinutes(double.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:OAuthTokenExpirationMinutes")));

            app.Properties.Add("eumis.AuthExpirationTimeSpan", expiresIn);

            var allowInsecureHttp = bool.Parse(ConfigurationManager.AppSettings.GetWithEnv("Eumis.Authentication:OAuthAllowInsecureHttp"));

            app.Properties.Add("eumis.OAuthAllowInsecureHttp", allowInsecureHttp);

            // set the eumis configuration properties in every request environment
            app.Use((ctx, next) =>
            {
                ctx.Set<TicketDataFormat>("eumis.TicketDataFormat", ticketDataFormat);
                ctx.Set<TimeSpan>("eumis.AuthExpirationTimeSpan", expiresIn);
                ctx.Set<bool>("eumis.OAuthAllowInsecureHttp", allowInsecureHttp);

                return next();
            });

            return app;
        }
    }
}