using Autofac;
using Eumis.Common.Log;
using Eumis.Log.ActionLogger;
using Eumis.Log.Owin;
using Microsoft.Owin;

namespace Eumis.Log
{
    public class LogModule : Module
    {
        private ILoggerFactory loggerFactory;

        public LogModule(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<ActionLogger.ActionLogger>().As<IActionLogger>().InstancePerRequest();

            moduleBuilder.Register(c =>
            {
                if (c.TryResolve<IOwinContext>(out IOwinContext owinContext))
                {
                    return owinContext.Get<ILogger>(LoggingMiddleware.LoggerOwinEnvKey);
                }
                else
                {
                    return this.loggerFactory.Create();
                }
            });
        }
    }
}
