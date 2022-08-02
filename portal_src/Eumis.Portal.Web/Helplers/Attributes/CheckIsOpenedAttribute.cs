using System;
using System.Web.Mvc;
using System.Web.Routing;
using Eumis.Components.Web;

namespace Eumis.Portal.Web.Helpers.Attributes
{
    public class CheckIsOpenedAttribute : ActionFilterAttribute
    {
        public string KeyName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AppContext.Current != null 
                && AppContext.Current.WorkingDocument != null 
                && AppContext.Current.WorkingDocument.gid == new Guid(filterContext.ActionParameters[KeyName].ToString()))
            {
                ViewResult viewResult = new ViewResult()
                {
                    ViewName = MVC.Shared.Views.Warning,
                    ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                    {
                        Model = (object)"Избраният документ е в процес на редакция."
                    }
                };

                filterContext.Result = viewResult;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}