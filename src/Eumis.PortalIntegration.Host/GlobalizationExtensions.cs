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

namespace Eumis.PortalIntegration.Host
{
    public static class GlobalizationExtensions
    {
        public static IAppBuilder UseGlobalization(this IAppBuilder app)
        {
            app.Use(new Func<AppFunc, AppFunc>(next => (env) =>
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(SystemLocalization.DefaultCulture);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(SystemLocalization.DefaultCulture);

                return next(env);
            }));

            return app;
        }
    }
}
