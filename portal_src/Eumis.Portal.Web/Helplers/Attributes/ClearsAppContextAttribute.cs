using System.Web.Mvc;
using Eumis.Components.Web;

namespace Eumis.Portal.Web.Helpers.Attributes
{
    public class ClearsAppContextAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AppContext.Current != null)
            {
                AppContext.Current.Clear();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}