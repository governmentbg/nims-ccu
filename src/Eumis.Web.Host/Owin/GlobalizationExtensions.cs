using Eumis.Common.Localization;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Eumis.Web.Host.Owin
{
    public static class GlobalizationExtensions
    {
        private const string CookieName = "preferred_culture";
        private static readonly string[] ValidCultures = new string[] { SystemLocalization.Bg_BG, SystemLocalization.En_GB };
        private static readonly IEnumerable<PathString> CulturePaths = ValidCultures.Select(c => new PathString($"/{c}/"));
        private static readonly string CultureRegexParts = ValidCultures.Select(c => $"(?:{c})").Aggregate((c1, c2) => $"{c1}|{c2}");
        private static readonly Regex CultureRegex = new Regex(CultureRegexParts, RegexOptions.Compiled);
        private static readonly Regex CulturePathRegex = new Regex($"^/({CultureRegexParts})/$", RegexOptions.Compiled);

        public static IAppBuilder UseGlobalization(this IAppBuilder app)
        {
            app.MapWhen(
                owinCtx => owinCtx.Request.Method == "POST"
                    && CulturePaths.Contains(owinCtx.Request.Path),
                branch =>
                {
                    branch.Use(new Func<AppFunc, AppFunc>(next => (env) =>
                    {
                        var ctx = new OwinContext(env);

                        var match = CulturePathRegex.Match(ctx.Request.Path.ToUriComponent());
                        var cultureName = match.Groups[1].Value;
                        ctx.Response.Cookies.Append(CookieName, cultureName);

                        if (Uri.TryCreate(ctx.Request.Headers["Referer"], UriKind.Absolute, out Uri referrerUri))
                        {
                            ctx.Response.Redirect(referrerUri.PathAndQuery);
                        }
                        else
                        {
                            ctx.Response.Redirect("/");
                        }

                        return Task.FromResult(0);
                    }));
                });
            app.Use(new Func<AppFunc, AppFunc>(next => (env) =>
            {
                var ctx = new OwinContext(env);
                var cultureCookie = ctx.Request.Cookies[CookieName];

                if (!string.IsNullOrEmpty(cultureCookie)
                    && CultureRegex.IsMatch(cultureCookie))
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCookie);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCookie);
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(SystemLocalization.DefaultCulture);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(SystemLocalization.DefaultCulture);
                }

                return next(env);
            }));

            return app;
        }
    }
}