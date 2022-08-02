using Eumis.Common.Helpers;
using NLog;
using System.Web.Http.Filters;

namespace Eumis.Common.Filters
{
    public class NLogApiExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Logger.Error(Helper.GetDetailedExceptionInfo(actionExecutedContext.Exception));

            base.OnException(actionExecutedContext);
        }
    }
}
