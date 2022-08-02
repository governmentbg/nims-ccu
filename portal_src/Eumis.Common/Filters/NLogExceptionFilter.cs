using Eumis.Common.Helpers;
using NLog;
using System;
using System.Text;
using System.Web.Mvc;
namespace Eumis.Common.Filters
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
