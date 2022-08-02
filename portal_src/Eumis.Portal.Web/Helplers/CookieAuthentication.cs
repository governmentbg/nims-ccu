using Eumis.Common.Config;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Eumis.Portal.Web.Helplers
{
    public static class CookieAuthenticationHelper
    {
        public static void UseReportCookieAuthentication(this IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = EumisAuthenticationTypes.Report,
                SlidingExpiration = false,
                ExpireTimeSpan = ((SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState")).Timeout,
                CookieName = "__eumis__identity__report__",
                TicketDataFormat = new Microsoft.Owin.Security.DataHandler
                            .TicketDataFormat(
                                new AesDataProtector(
                                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:EnvironmentNameKey") + "/report",
                                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:OwinAesProtectorKey"),
                                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:OwinAesProtectorPreamble"))),
                Provider = new CookieAuthenticationProvider
                {
                    OnApplyRedirect = ctx =>
                    {
                        if (!IsAjaxRequest(ctx.Request))
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                }
            });
        }

        public static void UsePublicCookieAuthentication(this IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = EumisAuthenticationTypes.Public,
                SlidingExpiration = false,
                ExpireTimeSpan = ((SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState")).Timeout,
                CookieName = "__eumis__identity__",
                TicketDataFormat = new Microsoft.Owin.Security.DataHandler
                            .TicketDataFormat(
                                new AesDataProtector(
                                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:EnvironmentNameKey") + "/public",
                                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:OwinAesProtectorKey"),
                                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:OwinAesProtectorPreamble"))),
                Provider = new CookieAuthenticationProvider
                {
                    OnApplyRedirect = ctx =>
                    {
                        if (!IsAjaxRequest(ctx.Request))
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                }
            });
        }

        public static void UsePrivateCookieAuthentication(this IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = EumisAuthenticationTypes.Private,
                SlidingExpiration = false,
                ExpireTimeSpan = ((SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState")).Timeout,
                CookieName = "__eumis__identity__private__",
                TicketDataFormat = new Microsoft.Owin.Security.DataHandler
                            .TicketDataFormat(
                                new AesDataProtector(
                                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:EnvironmentNameKey") + "/private",
                                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:OwinAesProtectorKey"),
                                    ConfigurationManager.AppSettings.GetWithEnv("Eumis.Portal.Web:OwinAesProtectorPreamble"))),
                Provider = new CookieAuthenticationProvider
                {
                    OnApplyRedirect = ctx =>
                    {
                        if (!IsAjaxRequest(ctx.Request))
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                }
            });
        }

        private static bool IsAjaxRequest(IOwinRequest request)
        {
            return
                request.Path.StartsWithSegments(new PathString("/api"))
                        || request.Path.Value.ToLower().Contains(Constants.SaveDraftActionName.ToLower());
        }

    }
}
