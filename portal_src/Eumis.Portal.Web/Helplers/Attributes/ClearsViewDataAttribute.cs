using System.Web.Mvc;

namespace Eumis.Portal.Web.Helpers.Attributes
{
    public class ClearsViewDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller != null)
            {
                filterContext.Controller.TempData.Clear();
                filterContext.Controller.ViewData.Clear();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}