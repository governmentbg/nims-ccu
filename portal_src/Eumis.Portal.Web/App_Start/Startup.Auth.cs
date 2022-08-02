using Eumis.Common.Config;
using Eumis.Portal.Web.Helplers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Configuration;
using System.Web.Configuration;

namespace Eumis.Portal.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<EumisUserManager>(EumisUserManager.Create);
            app.MapWhen(ctx => ctx.Request.Path.Value.IndexOf("companies/SearchCompany", StringComparison.OrdinalIgnoreCase) > 0,
            api => 
            {
                api.UseReportCookieAuthentication();
                api.UsePublicCookieAuthentication();
                api.UsePrivateCookieAuthentication();
            });

            app.MapWhen(
                ctx => 
                    { 
                        return 
                            ctx.Request.Path.Value.IndexOf("private", StringComparison.OrdinalIgnoreCase) == -1
                                && ctx.Request.Path.Value.IndexOf("report", StringComparison.OrdinalIgnoreCase) == -1; 
                    },
                publicApp =>
                {
                    publicApp.CreatePerOwinContext<PublicSignInManager>(PublicSignInManager.Create);
                    publicApp.UsePublicCookieAuthentication();
                });
            
            app.MapWhen(
                ctx =>
                {
                    return ctx.Request.Path.Value.IndexOf("private", StringComparison.OrdinalIgnoreCase) > 0;
                },
                privateApp =>
                {
                    privateApp.CreatePerOwinContext<PrivateSignInManager>(PrivateSignInManager.Create);
                    privateApp.UsePrivateCookieAuthentication();
                });

            app.MapWhen(
                ctx =>
                {
                    return ctx.Request.Path.Value.IndexOf("report", StringComparison.OrdinalIgnoreCase) > 0;
                },
                reportApp =>
                {
                    reportApp.CreatePerOwinContext<ReportSignInManager>(ReportSignInManager.Create);
                    reportApp.UseReportCookieAuthentication();
                });
        }
    }
}
