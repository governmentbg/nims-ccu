using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Portal.Web.Helpers.Attributes
{
    public class ChecksSubmissionStepAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = (string)filterContext.RouteData.Values["action"];

            SubmissionStateStep currentActionStep = (SubmissionStateStep)System.Enum.Parse(typeof(SubmissionStateStep), actionName, true);

            RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
            if (SubmissionState.Current == null)
            {
                redirectTargetDictionary.Add("controller", MVC.Default.Name);
                redirectTargetDictionary.Add("action", MVC.Default.ActionNames.Index);
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
            else
            {
                if ((int)currentActionStep < (int)SubmissionState.Current.CurrentStep)
                {
                    

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
                else
                {
                    SubmissionState.Current.CurrentStep = currentActionStep;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}