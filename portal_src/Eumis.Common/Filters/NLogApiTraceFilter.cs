using NLog;
using System.Web.Http.Filters;

namespace Eumis.Common.Filters
{
    public class NLogApiTraceFilter : ActionFilterAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Logger.Info(string.Empty);

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
