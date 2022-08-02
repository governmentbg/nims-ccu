using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Portal.Web.Helpers.Attributes
{
    public class AuthenticatedAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var areaContext = filterContext.RouteData.DataTokens["area"]?.ToString();
            if (string.IsNullOrEmpty(areaContext) || areaContext == MVC.Private.Name)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }

            RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
            
            if (areaContext == MVC.Report.Name)
            {
                
                redirectTargetDictionary.Add("controller", MVC.Report.Account.Name);
                redirectTargetDictionary.Add("action", MVC.Report.Account.ActionNames.Login);
                redirectTargetDictionary.Add("area", MVC.Report.Name);
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
            else
            {
                redirectTargetDictionary.Add("controller", MVC.Account.Name);
                redirectTargetDictionary.Add("action", MVC.Account.ActionNames.Login);
                redirectTargetDictionary.Add("area", string.Empty);
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
        }
    }
}
