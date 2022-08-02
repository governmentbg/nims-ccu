using Eumis.Common.Log;
using Eumis.Log.Owin;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;

namespace Eumis.Log.Api
{
    public class LoggingMiddlewareExceptionLogger : ExceptionLogger
    {
        private Type[] knownExceptionTypes;

        public LoggingMiddlewareExceptionLogger()
        {
            this.knownExceptionTypes = Array.Empty<Type>();
        }

        public LoggingMiddlewareExceptionLogger(Type[] knownExceptionTypes)
        {
            this.knownExceptionTypes = knownExceptionTypes;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            if (!this.knownExceptionTypes.Contains(context.Exception.GetType()))
            {
                context.Request.GetOwinContext()
                    .Get<ILogger>(LoggingMiddleware.LoggerOwinEnvKey)
                    .Log(LogLevel.Error, context.Exception.Message, context.Exception);
            }
        }
    }
}
