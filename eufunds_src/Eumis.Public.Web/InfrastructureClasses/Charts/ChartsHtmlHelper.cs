using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eumis.Public.Web.InfrastructureClasses.Charts
{
    public static class ChartsHtmlHelper
    {
        public static MvcHtmlString Chart(this HtmlHelper html, UrlHelper url, ChartRendererModel chart, object htmlAttributes = null)
        {
            if (chart == null)
            {
                throw new ArgumentNullException(nameof(chart));
            }

            return ChartRender(html, url, chart, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        private static MvcHtmlString ChartRender(HtmlHelper html, UrlHelper url, ChartRendererModel chart, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tb = new TagBuilder("div");

            tb.Attributes.Add("data-chart", "true");
            tb.Attributes.Add("data-title", chart.Title);
            tb.Attributes.Add("data-ytitle", chart.YTitle);
            tb.Attributes.Add("data-isstacked", chart.IsStacked.ToString());
            tb.Attributes.Add("data-hasstacklabels", chart.HasStackLabels.ToString());
            tb.Attributes.Add("data-dataurl", url.Action(chart.DataUrl));
            tb.MergeAttributes(htmlAttributes);

            tb.InnerHtml = TooltipHtmlHelper.RenderTooltip(html, chart.TooltipView);

            return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
        }
    }
}