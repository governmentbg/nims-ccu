using System;
using System.Web.Mvc;

namespace Eumis.Public.Web.InfrastructureClasses
{
    public class ClearRouteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            filterContext.RouteData.Values[RouteConfig.OPABB] = 0;
            filterContext.RouteData.Values[RouteConfig.PRABB] = 0;

            base.OnActionExecuting(filterContext);
        }
    }
}