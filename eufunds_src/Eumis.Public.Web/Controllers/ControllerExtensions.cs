﻿using System;
using System.IO;
using System.Web.Mvc;

namespace Eumis.Public.Web.Controllers
{
    public static class ControllerExtensions
    {
        public static string RenderView(this Controller controller, string viewName, object model)
        {
            return RenderView(controller, viewName, new ViewDataDictionary(model));
        }

        public static string RenderView(this Controller controller, string viewName, ViewDataDictionary viewData)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            var controllerContext = controller.ControllerContext;

            var viewResult = ViewEngines.Engines.FindView(controllerContext, viewName, null);

            StringWriter stringWriter;

            using (stringWriter = new StringWriter())
            {
                var viewContext = new ViewContext(
                    controllerContext,
                    viewResult.View,
                    viewData,
                    controllerContext.Controller.TempData,
                    stringWriter);

                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
            }

            return stringWriter.ToString();
        }
    }
}