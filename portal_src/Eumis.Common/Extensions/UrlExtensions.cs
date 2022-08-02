using Eumis.Common.Helpers;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Common.Extensions
{
    /// <summary>
    /// Клас, съдържащ помощни методи за UrlHelper
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// Помощен метод за рендиране на ActionMethod
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        /// <param name="confirmMessage">съобщение за потвърждение</param>
        /// <param name="confirmTitle">заглавие на потвърждение</param>
        /// <param name="disableAfterClick">флаг, показващ дали елемента се деактивира след заявката</param>
        /// <param name="actionIsSendOnly">флаг, показващ типа на заявката</param>
        /// <returns></returns>
        public static string ActionMethod(this UrlHelper urlHelper,
            string actionName,
            string controllerName = null,
            RouteValueDictionary routeValues = null,
            FormMethod formMethod = FormMethod.Get,
            string formId = null,
            string confirmMessage = null,
            string confirmTitle = null,
            bool disableAfterClick = true,
            bool actionIsSendOnly = false)
        {
            return ActionMethodInternal(urlHelper, actionName, controllerName, routeValues,
                formMethod, formId, confirmMessage, confirmTitle, disableAfterClick, actionIsSendOnly);
        }
              
        public static string MakeSortUrl(this UrlHelper urlHelper, string sortBy, string sortByKey, string sortOrderKey)
        {
            var routeValues = GetRouteValues(urlHelper);
            var sameSort = false;

            if (routeValues.TryGetValue(sortByKey, out object currentSortBy))
            {
                sameSort = sortBy == currentSortBy.ToString();
            }

            routeValues[sortByKey] = sortBy;

            if (routeValues.TryGetValue(sortOrderKey, out object sortOrder))
            {
                routeValues[sortOrderKey] = sameSort && (SortOrder)Enum.Parse(typeof(SortOrder), sortOrder.ToString(), ignoreCase: true) == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                routeValues[sortOrderKey] = SortOrder.Ascending;
            }            

            return urlHelper.RouteUrl(routeValues);
        }

        /// <summary>
        /// Рендира ActionMethod
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        /// <param name="confirmMessage">съобщение за потвърждение</param>
        /// <param name="confirmTitle">заглавие на потвърждение</param>
        /// <param name="disableAfterClick">флаг, показващ дали елемента се деактивира след заявката</param>
        /// <param name="actionIsSendOnly">флаг, показващ типа на заявката</param>
        /// <returns></returns>
        private static string ActionMethodInternal(this UrlHelper urlHelper, string actionName,
            string controllerName, RouteValueDictionary routeValues, FormMethod formMethod,
            string formId, string confirmMessage, string confirmTitle, bool disableAfterClick, bool actionIsSendOnly)
        {
            string url = urlHelper.Action(actionName, controllerName, routeValues);
            string method = HtmlHelper.GetFormMethodString(formMethod);

            string onclick = String.Format("$.submitPage('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, this); return false;", url, method,
                String.IsNullOrEmpty(formId) ? "undefined" : "'" + formId + "'",
                String.IsNullOrEmpty(confirmMessage) ? "undefined" : "'" + confirmMessage + "'",
                String.IsNullOrEmpty(confirmTitle) ? "undefined" : "'" + confirmTitle + "'",
                actionIsSendOnly ? "true" : "undefined",
                disableAfterClick ? "true" : "undefined");

            return onclick;
        }

        private static RouteValueDictionary GetRouteValues(UrlHelper urlHelper)
        {
            var routeValueDictionary = new RouteValueDictionary(urlHelper.RequestContext.RouteData.Values);
            var queryString = urlHelper.RequestContext.HttpContext.Request.QueryString;
            foreach (var key in queryString.AllKeys)
            {
                routeValueDictionary.Add(key, queryString[key]);
            };

            return routeValueDictionary;
        }
    }
}
