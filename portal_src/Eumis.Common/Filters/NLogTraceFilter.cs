using NLog;
using System.Web.Mvc;
namespace Eumis.Common.Filters
{
    public class NLogTraceFilter : System.Web.Mvc.ActionFilterAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Logger.Info(string.Empty);

            base.OnActionExecuted(filterContext);
        }
    }
}
