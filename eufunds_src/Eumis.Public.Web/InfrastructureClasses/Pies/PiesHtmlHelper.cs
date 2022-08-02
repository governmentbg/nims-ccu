using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eumis.Public.Web.InfrastructureClasses.Pies
{
    public static class PiesHtmlHelper
    {
        public static MvcHtmlString Pie(this HtmlHelper html, UrlHelper url, PieRendererModel pie, object htmlAttributes = null)
        {
            if (pie == null)
            {
                throw new ArgumentNullException(nameof(pie));
            }

            return PieRender(html, url, pie, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        private static MvcHtmlString PieRender(HtmlHelper html, UrlHelper url, PieRendererModel pie, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tb = new TagBuilder("div");

            tb.Attributes.Add("data-pie", "true");
            tb.Attributes.Add("data-title", pie.Title);
            tb.Attributes.Add("data-dataurl", url.Action(pie.DataUrl));
            tb.Attributes.Add("data-percentage-label-enabled", pie.PercentageLabelEnabled.ToString().ToLower());
            tb.MergeAttributes(htmlAttributes);

            tb.InnerHtml = TooltipHtmlHelper.RenderTooltip(html, pie.TooltipView);

            return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
        }
    }
}