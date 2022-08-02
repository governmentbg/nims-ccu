using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eumis.Public.Resources;

namespace Eumis.Public.Web.InfrastructureClasses
{
    public static class Utils
    {
        public static MvcHtmlString Breadcrumb(this HtmlHelper helper, List<BreadcrumbItem> items, string currentActionLabel)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            bool hasItems = items != null && items.Count > 0;

            var content = new TagBuilder("div");
            content.AddCssClass("breadcrumbs clearfix");

            if (hasItems)
            {
                var lastItem = items.Last();

                var img = new TagBuilder("img");
                img.Attributes.Add("src", Links.Content.img.icons.arrow_back_png);
                img.Attributes.Add("alt", Texts.Global_Back);

                var back = new TagBuilder("a");
                back.AddCssClass("back");
                back.Attributes.Add("href", lastItem.Action);
                back.Attributes.Add("title", Texts.Global_Back);

                back.InnerHtml = img.ToString(TagRenderMode.SelfClosing);

                content.InnerHtml += back.ToString();
            }

            var span = new TagBuilder("span");

            content.InnerHtml += span.ToString();

            var div = new TagBuilder("div");
            if (hasItems)
            {
                foreach (var item in items)
                {
                    var itemSpan = new TagBuilder("span");
                    itemSpan.InnerHtml = item.Label;

                    var itemLink = new TagBuilder("a");
                    itemLink.Attributes.Add("href", item.Action);
                    itemLink.Attributes.Add("title", item.Label);
                    itemLink.InnerHtml = itemSpan.ToString();

                    var itemDiv = new TagBuilder("div");
                    itemDiv.InnerHtml = itemLink.ToString();

                    div.InnerHtml += itemDiv.ToString();

                    var itemImg = new TagBuilder("img");
                    itemImg.Attributes.Add("src", Links.Content.img.icons.breadcrumb_sep_png);
                    div.InnerHtml += itemImg.ToString(TagRenderMode.SelfClosing);
                }

                var activeSpan = new TagBuilder("span");
                activeSpan.InnerHtml = currentActionLabel;

                var activeLink = new TagBuilder("a");
                activeLink.AddCssClass("active");
                activeLink.InnerHtml = activeSpan.ToString();

                var currentDiv = new TagBuilder("div");
                currentDiv.InnerHtml = activeLink.ToString();

                div.InnerHtml += currentDiv.ToString();
            }

            content.InnerHtml += div.ToString();

            var container = new TagBuilder("div");
            container.AddCssClass("container");
            container.InnerHtml = content.ToString();

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("bg_breadcrumbs hidden-xs hidden-sm");
            wrapper.InnerHtml = container.ToString();

            return MvcHtmlString.Create(wrapper.ToString());
        }
    }
}