using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Eumis.Public.Web.InfrastructureClasses.Maps
{
    public static class MapsHtmlHelper
    {
        public static MvcHtmlString BgMap(this HtmlHelper html, UrlHelper url, BgMapRendererModel map, object htmlAttributes = null)
        {
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            return Map(html, url, map, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        private static MvcHtmlString Map(HtmlHelper html, UrlHelper url, MapRendererModel map, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tb = new TagBuilder("div");

            tb.Attributes.Add("data-map", "true");
            tb.Attributes.Add("data-type", map.Type);
            tb.Attributes.Add("data-rootid", map.RootId.ToString());
            tb.Attributes.Add("data-mapid", (map.ShowId.HasValue ? map.ShowId.Value : map.RootId).ToString());
            tb.Attributes.Add("data-baseurl", map.BaseUrl.ToString());
            tb.Attributes.Add("data-redirectitemurl", url.Action(map.RedirectUrl));
            tb.Attributes.Add("data-infoset", map.Infoset.ToString());
            tb.MergeAttributes(htmlAttributes);

            tb.InnerHtml = TooltipHtmlHelper.RenderTooltip(html, map.TooltipView);

            return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
        }
    }
}