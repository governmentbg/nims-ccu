using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Public.Common.Helpers
{
    /// <summary>
    /// Клас, съдържащ помощни методи за HtmlHelper.
    /// </summary>
    public static class HtmlExtensions
    {
        #region Localization

        public static string LocalResources(this WebViewPage page, string key)
        {
            return page.ViewContext.HttpContext.GetLocalResourceObject(page.VirtualPath, key) as string;
        }

        public static string LanguageUrl(this HtmlHelper helper, string cultureName, string languageRouteName = "lang")
        {
            cultureName = cultureName.ToLower();
            var routeValues = new RouteValueDictionary(helper.ViewContext.RouteData.Values);

            var queryString = helper.ViewContext.HttpContext.Request.QueryString;

            foreach (string key in queryString)
            {
                if (queryString[key] != null && !string.IsNullOrWhiteSpace(key))
                {
                    if (routeValues.ContainsKey(key))
                    {
                        routeValues[key] = queryString[key];
                    }
                    else
                    {
                        routeValues.Add(key, queryString[key]);
                    }
                }
            }

            routeValues[languageRouteName] = cultureName;

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string url = urlHelper.RouteUrl(routeValues);

            return url;
        }

        #endregion
    }
}
