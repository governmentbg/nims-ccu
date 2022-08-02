using System.Web.Mvc;
using Eumis.Public.Common.Helpers;
using NLog;

namespace Eumis.Public.Common.NLog.Filters
{
    public class NLogExceptionFilter : HandleErrorAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnException(ExceptionContext filterContext)
        {
            Logger.Error(Helper.GetDetailedExceptionInfo(filterContext.Exception));

            base.OnException(filterContext);
        }
    }
}
