using Eumis.Common.Resources;
using Eumis.Portal.Web.Areas.Private;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Portal.Web.Helplers.Filters
{
    public class PrivateAreaExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if ((string)(filterContext.RouteData.DataTokens["area"] ?? string.Empty) == PrivateAreaRegistration.Name)
            {
                var masterName = string.Empty;
                var area = (string)(filterContext.RouteData.DataTokens["area"] ?? string.Empty);

                switch (area)
                {
                    case PrivateAreaRegistration.Name:
                        masterName = MVC.Private.Shared.Views._Layout;
                        break;
                    default:
                        masterName = string.Empty;
                        break;
                }

                ViewResult viewResult = new ViewResult()
                {
                    ViewName = MVC.Shared.Views.Failure,
                    ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                    {
                        Model = (object)Global.ErrorTryAgain
                    },
                    MasterName = masterName
                };

                filterContext.Result = viewResult;

                filterContext.ExceptionHandled = true;
            }
        }
    }
}
