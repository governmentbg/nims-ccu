using Eumis.Common.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Api
{
    public static class AppBuilderExtension
    {
        public static void UseLoggingMiddleware(this IAppBuilder app)
        {
            app.Use<LoggingMiddleware>();
        }

        public static void UseRequestLoggingMiddleware(this IAppBuilder app)
        {
            app.Use<RequestLoggingMiddleware>();
        }
    }
}
