using System.Net;
using Eumis.Common.Resources;
using Eumis.Portal.Web.Areas.Private;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Eumis.Portal.Web.Areas.Report;
using Eumis.Common.Localization;

namespace Eumis.Portal.Web.Helplers.Filters
{
    public class ServiceUnavailableFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is WebException)
            {
                var exception = ((WebException)filterContext.Exception);

                if ((exception.Response != null 
                            && ((HttpWebResponse)exception.Response).StatusCode == HttpStatusCode.ServiceUnavailable)
                        || exception.Status == WebExceptionStatus.ConnectFailure)
                {
                    var masterName = string.Empty;

                    var controllerName = filterContext.RouteData.Values["controller"];

                    if (filterContext.RouteData.Values["controller"].ToString() == "Public")
                    {
                        masterName = MVC.Shared.Views._PublicLayout;
                    }
                    else
                    {
                        var area = (string)(filterContext.RouteData.DataTokens["area"] ?? string.Empty);

                        switch (area)
                        {
                            case ReportAreaRegistration.Name:
                                masterName = MVC.Report.Shared.Views._Layout;
                                break;
                            case PrivateAreaRegistration.Name:
                                masterName = MVC.Private.Shared.Views._Layout;
                                break;
                            default:
                                masterName = string.Empty;
                                break;
                        }
                    }

                    object message = "Системата е в процес на обновяване. Моля, опитайте отново по-късно.";

                    if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                    {
                        message = "The system is in the process of being upgraded. Please try again later.";
                    }

                    ViewResult viewResult = new ViewResult()
                    {
                        ViewName = MVC.Shared.Views.Warning,
                        ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                        {
                            Model = message
                        },
                        MasterName = masterName
                    };

                    filterContext.Result = viewResult;
                    
                    filterContext.ExceptionHandled = true;
                }
            }
        }
    }
}
