using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Portal.Web.Helpers.Attributes
{
    public class IsAuthenticatedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("controller", MVC.Account.Name);
                redirectTargetDictionary.Add("action", MVC.Account.ActionNames.ProfileEdit);
                redirectTargetDictionary.Add("area", string.Empty);
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}