using System.Web.Mvc;
using NLog;

namespace Eumis.Public.Common.NLog.Filters
{
    public class NLogTraceFilter : ActionFilterAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Logger.Info(string.Empty);

            base.OnActionExecuted(filterContext);
        }
    }
}
