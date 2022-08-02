using System.Web.Mvc;

namespace Eumis.Portal.Web.Helpers
{
    public class AppContextViewEngine : RazorViewEngine
    {
        public AppContextViewEngine()
        {
            MasterLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            AreaMasterLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
            };

            ViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
            };

            AreaViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
            };

            PartialViewLocationFormats = ViewLocationFormats;
            AreaPartialViewLocationFormats = AreaViewLocationFormats;
        }

        //public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        //{
        //    if (AppContext.Current != null && controllerContext.Controller.GetType().Name == "MainController")
        //    {
        //        string originalControllerName = controllerContext.RouteData.GetRequiredString("controller");
        //        controllerContext.RouteData.Values["controller"] = AppContext.Current.AppRioCode ?? AppContext.Current.GetApplication().DocumentMetadata.RioCode;

        //        ViewEngineResult result = base.FindView(controllerContext, viewName, masterName, useCache);

        //        controllerContext.RouteData.Values["controller"] = originalControllerName;

        //        return result;
        //    }
        //    else if (AppContext.Current != null && controllerContext.Controller.GetType().Name == "UploadController")
        //    {
        //        string originalControllerName = controllerContext.RouteData.GetRequiredString("controller");
        //        controllerContext.RouteData.Values["controller"] = AppContext.Current.AppRioCode ?? AppContext.Current.GetApplication().DocumentMetadata.RioCode;

        //        ViewEngineResult result = base.FindView(controllerContext, viewName, masterName, useCache);

        //        controllerContext.RouteData.Values["controller"] = originalControllerName;

        //        return result;
        //    }
        //    else
        //        return new ViewEngineResult(new string[] { });
        //}

        //public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        //{
        //    if (AppContext.Current != null && controllerContext.Controller.GetType().Name == "MainController")
        //    {
        //        string originalControllerName = controllerContext.RouteData.GetRequiredString("controller");
        //        controllerContext.RouteData.Values["controller"] = AppContext.Current.AppRioCode ?? AppContext.Current.GetApplication().DocumentMetadata.RioCode;

        //        ViewEngineResult result = base.FindPartialView(controllerContext, partialViewName, useCache);

        //        controllerContext.RouteData.Values["controller"] = originalControllerName;

        //        return result;
        //    }
        //    else if (AppContext.Current != null && controllerContext.Controller.GetType().Name == "UploadController")
        //    {
        //        string originalControllerName = controllerContext.RouteData.GetRequiredString("controller");
        //        controllerContext.RouteData.Values["controller"] = AppContext.Current.AppRioCode ?? AppContext.Current.GetApplication().DocumentMetadata.RioCode;

        //        ViewEngineResult result = base.FindPartialView(controllerContext, partialViewName, useCache);

        //        controllerContext.RouteData.Values["controller"] = originalControllerName;

        //        return result;
        //    }
        //    else
        //        return new ViewEngineResult(new string[] { });            
        //}
    }
}