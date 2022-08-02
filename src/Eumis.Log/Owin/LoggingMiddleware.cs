using Eumis.Common.Log;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Eumis.Log.Owin
{
    public class LoggingMiddleware
    {
        public const string LoggerOwinEnvKey = "eumis.Logger";
        public const string TimerTicksAtEntryOwinEnvKey = "eumis.TimerTicksAtEntry";

        private AppFunc next;
        private ILoggerFactory loggerFactory;

        public LoggingMiddleware(AppFunc next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this.loggerFactory = loggerFactory;
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            // create the logger for this request
            var owinContext = new OwinContext(env);
            var logger = this.loggerFactory.Create(owinContext);
            owinContext.Set(LoggerOwinEnvKey, logger);
            owinContext.Set(TimerTicksAtEntryOwinEnvKey, Stopwatch.GetTimestamp());

            return this.next(env).ContinueWith(
                appTask =>
                {
                    if (appTask.IsFaulted && appTask.Exception != null)
                    {
                        foreach (var innerException in appTask.Exception.InnerExceptions)
                        {
                            logger.Log(LogLevel.Error, innerException.Message, innerException);
                        }

                        throw appTask.Exception;
                    }
                    else
                    {
                        logger.Log(LogLevel.Info, null, null);
                    }

                    return appTask;
                },
                TaskScheduler.Current);
        }
    }
}
