using Microsoft.Owin;
using NLog;
using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Eumis.Common.Owin
{
    public class LoggingMiddleware : OwinMiddleware
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LoggingMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            try
            {
                await this.Next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw;
            }

            return;
        }
    }
}
