using System;
using System.IO;
using System.Web.Mvc;

namespace Eumis.Public.Web.InfrastructureClasses
{
    public static class TooltipHtmlHelper
    {
        public static string RenderTooltip(HtmlHelper html, string tooltipView)
        {
            if (html == null)
            {
                throw new ArgumentNullException(nameof(html));
            }

            TagBuilder tooltip = new TagBuilder("div");
            tooltip.Attributes.Add("style", "display:none;");
            tooltip.Attributes.Add("data-tooltip", "true");
            tooltip.InnerHtml = RenderPartialToString(html, tooltipView);

            return tooltip.ToString(TagRenderMode.Normal);
        }

        private static string RenderPartialToString(HtmlHelper helper, string partialViewName)
        {
            var controllerContext = helper.ViewContext.Controller.ControllerContext;
            var viewData = helper.ViewData;
            var tempData = helper.ViewContext.TempData;

            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, partialViewName);

                var viewContext = new ViewContext(controllerContext, viewResult.View, viewData, tempData, stringWriter);

                viewResult.View.Render(viewContext, stringWriter);

                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}