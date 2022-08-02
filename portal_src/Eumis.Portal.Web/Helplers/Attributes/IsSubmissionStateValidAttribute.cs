using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Portal.Web.Helpers.Attributes
{
    public class IsSubmissionStateValidAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SubmissionState.Current == null || !SubmissionState.Current.IsProjectValid)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();

                if (SubmissionState.Current.IsMessageSubmission)
                {
                    redirectTargetDictionary.Add("controller", MVC.Message.Name);
                    redirectTargetDictionary.Add("action", MVC.Message.ActionNames.Index);
                }
                else
                {
                    redirectTargetDictionary.Add("controller", MVC.Submit.Name);
                    redirectTargetDictionary.Add("action", MVC.Submit.ActionNames.Disclaimer);
                }

                redirectTargetDictionary.Add("area", string.Empty);
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}