using Microsoft.Owin;
using Owin;

namespace Eumis.Log.Owin
{
    public static class LoggingExtensions
    {
        private const string OWIN_CONTEXT_CONTAINS_SENSITIVE_DATA = "eumis.OwinContextContainsSensitiveData";

        public static IAppBuilder UseLogging(this IAppBuilder app, ILoggerFactory loggerFactory)
        {
            return app.Use<LoggingMiddleware>(loggerFactory);
        }

        public static void SetContainsSensitiveData(this IOwinContext context)
        {
            context.Set(OWIN_CONTEXT_CONTAINS_SENSITIVE_DATA, true);
        }

        public static bool ContainsSensitiveData(this IOwinContext context)
        {
            return context.Get<bool>(OWIN_CONTEXT_CONTAINS_SENSITIVE_DATA);
        }
    }
}
