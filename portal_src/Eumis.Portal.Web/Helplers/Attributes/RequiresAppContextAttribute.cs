using System.Web.Mvc;
using System.Web.Routing;
using Eumis.Components.Web;

namespace Eumis.Portal.Web.Helpers.Attributes
{
    public class RequiresAppContextAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AppContext.Current == null)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("controller", MVC.Default.Name);
                redirectTargetDictionary.Add("action", MVC.Default.ActionNames.Index);
                redirectTargetDictionary.Add("area", string.Empty);
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}