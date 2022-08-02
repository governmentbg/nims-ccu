using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using Eumis.Common.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using System.Web;

namespace Eumis.Common.Extensions
{
    /// <summary>
    /// Клас, съдържащ помощни методи за HtmlHelper
    /// </summary>
    public static class _HtmlExtensions
    {
        #region ActionButton

        /// <summary>
        /// Помощен метод за рендиране на ActionButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемент</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        /// <param name="confirmMessage">съобщение за потвърждение</param>
        /// <param name="confirmTitle">заглавие на потвърждение</param>
        /// <param name="disableAfterClick">флаг, показващ дали елемента се деактивира след заявката</param>
        /// <param name="actionIsSendOnly">флаг, показващ типа на заявката</param>
        /// <returns></returns>
        public static MvcHtmlString ActionButton(this HtmlHelper htmlHelper,
            string name,
            string value,
            string actionName,
            string controllerName = null,
            object htmlAttributes = null,
            object routeValues = null,
            FormMethod formMethod = FormMethod.Post,
            string formId = null,
            string confirmMessage = null,
            string confirmTitle = null,
            bool disableAfterClick = true,
            bool actionIsSendOnly = false)
        {
            return MvcHtmlString.Create(ActionButtonInternal(htmlHelper, name, value, new RouteValueDictionary(htmlAttributes),
                actionName, controllerName, new RouteValueDictionary(routeValues), formMethod,
                formId, confirmMessage, confirmTitle, disableAfterClick, actionIsSendOnly));
        }

        /// <summary>
        /// Рендира ActionButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемент</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        /// <param name="confirmMessage">съобщение за потвърждение</param>
        /// <param name="confirmTitle">заглавие на потвърждение</param>
        /// <param name="disableAfterClick">флаг, показващ дали елемента се деактивира след заявката</param>
        /// <param name="actionIsSendOnly">флаг, показващ типа на заявката</param>
        /// <returns></returns>
        private static string ActionButtonInternal(this HtmlHelper htmlHelper, string name, string value,
            IDictionary<string, object> htmlAttributes, string actionName, string controllerName,
            RouteValueDictionary routeValues, FormMethod formMethod, string formId,
            string confirmMessage, string confirmTitle, bool disableAfterClick, bool actionIsSendOnly)
        {
            RouteValueDictionary mergedRouteValues = RouteValuesHelpers.MergeRouteValues(actionName,
                controllerName, htmlHelper.ViewContext.RequestContext.RouteData.Values, routeValues, true);

            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string onclick = urlHelper.ActionMethod(actionName, controllerName, routeValues, formMethod, formId, confirmMessage, confirmTitle, disableAfterClick, actionIsSendOnly);

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", "button", true);
            tagBuilder.MergeAttribute("name", name, true);
            tagBuilder.MergeAttribute("value", value, true);
            tagBuilder.MergeAttribute("onclick", onclick);

            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }

        #endregion        

        #region ActionLinkPrePostButton

        /// <summary>
        /// Помощен метод за начало на рендиране на ActionLinkButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемент</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        /// <returns></returns>
        public static MvcHtmlString BeginActionLinkPrePostButton(this HtmlHelper htmlHelper,
            string name,
            string value,
            string actionName,
            string controllerName = null,
            object htmlAttributes = null,
            object routeValues = null,
            FormMethod formMethod = FormMethod.Get,
            string formId = null)
        {
            return MvcHtmlString.Create(ActionLinkPrePostButtonInternal(htmlHelper, name, value, new RouteValueDictionary(htmlAttributes),
                actionName, controllerName, new RouteValueDictionary(routeValues), formMethod, formId));
        }

        /// <summary>
        /// Помощен метод за край на рендиране на ActionLinkButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString EndActionLinkPrePostButton(this HtmlHelper htmlHelper)
        {
            TagBuilder tagBuilder = new TagBuilder("a");
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.EndTag));
        }

        /// <summary>
        /// Помощен метод за на рендиране на ActionLinkButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемент</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        public static MvcHtmlString ActionLinkPrePostButton(this HtmlHelper htmlHelper,
            string name,
            string value,
            string actionName,
            string controllerName = null,
            object htmlAttributes = null,
            object routeValues = null,
            FormMethod formMethod = FormMethod.Get,
            string formId = null)
        {
            return MvcHtmlString.Create(ActionLinkPrePostButtonInternal(htmlHelper, name, value, new RouteValueDictionary(htmlAttributes),
                actionName, controllerName, new RouteValueDictionary(routeValues), formMethod, formId));
        }

        /// <summary>
        /// Рендира ActionLinkButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемент</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        private static string ActionLinkPrePostButtonInternal(this HtmlHelper htmlHelper, string name,
            string value, IDictionary<string, object> htmlAttributes, string actionName,
            string controllerName, RouteValueDictionary routeValues, FormMethod formMethod, string formId)
        {
            RouteValueDictionary mergedRouteValues = RouteValuesHelpers.MergeRouteValues(actionName,
                controllerName, htmlHelper.ViewContext.RequestContext.RouteData.Values, routeValues, true);

            TagBuilder tagBuilder = new TagBuilder("a");

            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            string onclick = urlHelper.PrePostActionMethod(actionName, controllerName, routeValues, formMethod, formId);

            tagBuilder.InnerHtml = (!String.IsNullOrEmpty(value)) ? HttpUtility.HtmlEncode(value) : String.Empty;

            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", name, true);
            tagBuilder.MergeAttribute("href", "#");
            tagBuilder.MergeAttribute("onclick", onclick);

            return tagBuilder.ToString(TagRenderMode.StartTag);
        }

        /// <summary>
        /// Помощен метод за рендиране на ActionMethod
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        /// <returns></returns>
        public static string PrePostActionMethod(this UrlHelper urlHelper,
            string actionName,
            string controllerName = null,
            RouteValueDictionary routeValues = null,
            FormMethod formMethod = FormMethod.Get,
            string formId = null)
        {
            return PrePostActionMethodInternal(urlHelper, actionName, controllerName, routeValues, formMethod, formId);
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
        /// <returns></returns>
        private static string PrePostActionMethodInternal(this UrlHelper urlHelper, string actionName,
            string controllerName, RouteValueDictionary routeValues, FormMethod formMethod, string formId)
        {
            string url = urlHelper.Action(actionName, controllerName, routeValues);
            string method = HtmlHelper.GetFormMethodString(formMethod);

            string callback = String.Format("function(){{ $.submitPage('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, this); }}", url, method,
                String.IsNullOrEmpty(formId) ? "undefined" : "'" + formId + "'", "undefined", "undefined", "undefined", "true");

            string onclick = String.Format("PARTIAL_SAVE_SECTIONS({0}); return false;", callback);

            return onclick;
        }

        #endregion

        #region ActionLinkButton

        /// <summary>
        /// Помощен метод за начало на рендиране на ActionLinkButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемент</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        /// <param name="confirmMessage">съобщение за потвърждение</param>
        /// <param name="confirmTitle">заглавие на потвърждение</param>
        /// <param name="disableAfterClick">флаг, показващ дали елемента се деактивира след заявката</param>
        /// <param name="actionIsSendOnly">флаг, показващ типа на заявката</param>
        /// <returns></returns>
        public static MvcHtmlString BeginActionLinkButton(this HtmlHelper htmlHelper,
            string name,
            string value,
            string actionName,
            string controllerName = null,
            object htmlAttributes = null,
            object routeValues = null,
            FormMethod formMethod = FormMethod.Get,
            string formId = null,
            string confirmMessage = null,
            string confirmTitle = null,
            bool disableAfterClick = true,
            bool actionIsSendOnly = false)
        {
            return MvcHtmlString.Create(ActionLinkButtonInternal(htmlHelper, name, value, new RouteValueDictionary(htmlAttributes),
                actionName, controllerName, new RouteValueDictionary(routeValues), formMethod, formId,
                confirmMessage, confirmTitle, disableAfterClick, actionIsSendOnly, true));
        }

        /// <summary>
        /// Помощен метод за край на рендиране на ActionLinkButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString EndActionLinkButton(this HtmlHelper htmlHelper)
        {
            TagBuilder tagBuilder = new TagBuilder("a");
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.EndTag));
        }

            /// <summary>
        /// Помощен метод за на рендиране на ActionLinkButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемент</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        /// <param name="confirmMessage">съобщение за потвърждение</param>
        /// <param name="confirmTitle">заглавие на потвърждение</param>
        /// <param name="disableAfterClick">флаг, показващ дали елемента се деактивира след заявката</param>
        /// <param name="actionIsSendOnly">флаг, показващ типа на заявката</param>
        public static MvcHtmlString ActionLinkButton(this HtmlHelper htmlHelper,
            string name,
            string value,
            string actionName,
            string controllerName = null,
            object htmlAttributes = null,
            object routeValues = null,
            FormMethod formMethod = FormMethod.Get,
            string formId = null,
            string confirmMessage = null,
            string confirmTitle = null,
            bool disableAfterClick = true,
            bool actionIsSendOnly = false)
        {
            return MvcHtmlString.Create(ActionLinkButtonInternal(htmlHelper, name, value, new RouteValueDictionary(htmlAttributes),
                actionName, controllerName, new RouteValueDictionary(routeValues), formMethod, formId,
                confirmMessage, confirmTitle, disableAfterClick, actionIsSendOnly, false));
        }

        /// <summary>
        /// Рендира ActionLinkButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемент</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на форма</param>
        /// <param name="confirmMessage">съобщение за потвърждение</param>
        /// <param name="confirmTitle">заглавие на потвърждение</param>
        /// <param name="disableAfterClick">флаг, показващ дали елемента се деактивира след заявката</param>
        /// <param name="actionIsSendOnly">флаг, показващ типа на заявката</param>
        /// <param name="renderStartTagOnly">флаг, показващ дали да се рендира само началния таг</param>
        private static string ActionLinkButtonInternal(this HtmlHelper htmlHelper, string name,
            string value, IDictionary<string, object> htmlAttributes, string actionName,
            string controllerName, RouteValueDictionary routeValues, FormMethod formMethod,
            string formId, string confirmMessage, string confirmTitle, bool disableAfterClick, bool actionIsSendOnly, bool renderStartTagOnly)
        {
            RouteValueDictionary mergedRouteValues = RouteValuesHelpers.MergeRouteValues(actionName,
                controllerName, htmlHelper.ViewContext.RequestContext.RouteData.Values, routeValues, true);

            TagBuilder tagBuilder = new TagBuilder("a");

            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            string onclick = urlHelper.ActionMethod(actionName, controllerName, routeValues, formMethod, formId, confirmMessage, confirmTitle, disableAfterClick, actionIsSendOnly);

            tagBuilder.InnerHtml = (!String.IsNullOrEmpty(value)) ? HttpUtility.HtmlEncode(value) : String.Empty;

            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", name, true);
            tagBuilder.MergeAttribute("href", "#");
            tagBuilder.MergeAttribute("onclick", onclick);

            return tagBuilder.ToString(renderStartTagOnly ? TagRenderMode.StartTag : TagRenderMode.Normal);
        }

        #endregion

        #region BeginEndLabel

        /// <summary>
        /// Помощен метод за начало на рендиране на Label елемент
        /// </summary>
        /// <typeparam name="TModel">тип на модела</typeparam>
        /// <typeparam name="TProperty">тип на свойството</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString BeginLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return BeginLabelFor(htmlHelper, expression, (IDictionary<string, object>)null);
        }

        /// <summary>
        /// Помощен метод за начало на рендиране на Label елемент
        /// </summary>
        /// <typeparam name="TModel">тип на модела</typeparam>
        /// <typeparam name="TProperty">тип на свойството</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <returns></returns>
        public static MvcHtmlString BeginLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return BeginLabelFor(htmlHelper, expression, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Помощен метод за начало на рендиране на Label елемент
        /// </summary>
        /// <typeparam name="TModel">тип на модела</typeparam>
        /// <typeparam name="TProperty">тип на свойството</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <returns></returns>
        public static MvcHtmlString BeginLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("label");

            tagBuilder.MergeAttributes(htmlAttributes);

            string name = ExpressionHelper.GetExpressionText(expression);
            name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            tagBuilder.MergeAttribute("for", name, true);

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.StartTag));
        }

        /// <summary>
        /// Помощен метод за край на рендиране на Label елемент
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString EndLabel(this HtmlHelper htmlHelper)
        {
            TagBuilder tagBuilder = new TagBuilder("label");
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.EndTag));
        }

        #endregion

        #region ActionRadioButton

        /// <summary>
        /// Помощен метод за рендиране на ActionRadioButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемента</param>
        /// <param name="isChecked">флаг, показващ дали елемента е маркиран</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на формата</param>
        /// <returns></returns>
        public static MvcHtmlString ActionRadioButton(this HtmlHelper htmlHelper,
            string name,
            string value,
            bool isChecked,
            string actionName,
            string controllerName = null,
            object htmlAttributes = null,
            object routeValues = null,
            FormMethod formMethod = FormMethod.Get,
            string formId = null)
        {
            return MvcHtmlString.Create(ActionRadioButtonInternal(htmlHelper, name, value, isChecked, new RouteValueDictionary(htmlAttributes),
                actionName, controllerName, new RouteValueDictionary(routeValues), formMethod, formId));
        }

        /// <summary>
        /// Помощен метод за рендиране на ActionRadioButton
        /// </summary>
        /// <typeparam name="TModel">тип на модела</typeparam>
        /// <typeparam name="TProperty">тип на свойството</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">expression до елемента</param>
        /// <param name="value">стойност на елемента</param>        
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на формата</param>
        /// <returns></returns>
        public static string ActionRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object value,
            string actionName,
            string controllerName = null,
            object htmlAttributes = null,
            object routeValues = null,
            FormMethod formMethod = FormMethod.Get,
            string formId = null)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            string valueString = Convert.ToString(value, CultureInfo.CurrentCulture);

            object model = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            bool isChecked = model != null &&
                            !String.IsNullOrEmpty(name) &&
                            String.Equals(model.ToString(), valueString, StringComparison.OrdinalIgnoreCase);

            return ActionRadioButtonInternal(htmlHelper, name, valueString, isChecked, new RouteValueDictionary(htmlAttributes),
                actionName, controllerName, new RouteValueDictionary(routeValues), formMethod, formId);
        }

        /// <summary>
        /// Рендира ActionRadioButton
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на елемента</param>
        /// <param name="value">стойност на елемента</param>
        /// <param name="isChecked">флаг, показващ дали елемента е маркиран</param>
        /// <param name="actionName">име на метода, към който се прави заявката</param>
        /// <param name="controllerName">име на контролер</param>
        /// <param name="htmlAttributes">атрибути на елемента</param>
        /// <param name="routeValues">параметри на заявката</param>
        /// <param name="formMethod">метод за подаване на заявка</param>
        /// <param name="formId">идентификатор на формата</param>
        /// <returns></returns>
        private static string ActionRadioButtonInternal(this HtmlHelper htmlHelper, string name,
            string value, bool isChecked, IDictionary<string, object> htmlAttributes, string actionName,
            string controllerName, RouteValueDictionary routeValues, FormMethod formMethod,
            string formId)
        {
            TagBuilder tagBuilder = new TagBuilder("input");

            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", "radio", true);
            tagBuilder.MergeAttribute("value", value, true);
            tagBuilder.MergeAttribute("name", name, true);

            if (isChecked)
            {
                tagBuilder.MergeAttribute("checked", "checked", true);
            }
            else
            {
                tagBuilder.Attributes.Remove("checked");

                RouteValueDictionary mergedRouteValues = RouteValuesHelpers.MergeRouteValues(actionName,
                    controllerName, htmlHelper.ViewContext.RequestContext.RouteData.Values, routeValues, true);

                UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

                string onclick = urlHelper.ActionMethod(actionName, controllerName, routeValues, formMethod, formId, null, null, false, false);
                tagBuilder.MergeAttribute("onclick", onclick);
            }

            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }

        #endregion

        #region GetNameFor

        /// <summary>
        /// Връща име на дадено свойство
        /// </summary>
        /// <typeparam name="TModel">тип на модела</typeparam>
        /// <typeparam name="TProperty">тип на свойството</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetNameFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            name = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            return name;
        }

        #endregion

        #region GetIdFor

        /// <summary>
        /// Връща идентификатор за дадено свойство
        /// </summary>
        /// <typeparam name="TModel">тип на модела</typeparam>
        /// <typeparam name="TProperty">тип на свойството</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            name = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(name);

            return name;
        }

        #endregion

        #region ViewDataFor

        /// <summary>
        /// Инициализира данни в хештаблица
        /// </summary>
        /// <typeparam name="TModel">тип на модела</typeparam>
        /// <typeparam name="TProperty">тип на свойството</typeparam>
        /// <param name="viewData">хештаблица</param>
        /// <param name="expression"></param>
        /// <param name="data">данни</param>
        public static void SetViewDataFor<TModel, TProperty>(this ViewDataDictionary viewData, Expression<Func<TModel, TProperty>> expression, object data)
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);

            viewData["__" + inputName] = data;
        }

        /// <summary>
        /// Инициализира данни в хештаблица
        /// </summary>
        /// <typeparam name="TModel">тип на модела</typeparam>
        /// <typeparam name="TTemplateModel">тип на темплейт модела</typeparam>
        /// <typeparam name="TProperty">тип на свойството</typeparam>
        /// <param name="viewData">хештаблица</param>
        /// <param name="templateExp">път до темплейта</param>
        /// <param name="subTemplateExp">път до под-темплейта</param>
        /// <param name="data">данни</param>
        public static void SetViewDataFor<TModel, TTemplateModel, TProperty>(this ViewDataDictionary viewData, Expression<Func<TModel, TTemplateModel>> templateExp, Expression<Func<TTemplateModel, TProperty>> subTemplateExp, object data)
        {
            string inputName = ExpressionHelper.GetExpressionText(templateExp) + "." + ExpressionHelper.GetExpressionText(subTemplateExp);

            viewData["__" + inputName] = data;
        }

        /// <summary>
        /// Връща данни от хештаблица
        /// </summary>
        /// <param name="viewData">хештаблица</param>
        /// <returns></returns>
        public static object GetViewDataForTemplate(this ViewDataDictionary viewData)
        {
            string name = viewData.TemplateInfo.GetFullHtmlFieldName("");

            return viewData["__" + name];
        }

        /// <summary>
        /// Връща данни от хештаблица
        /// </summary>
        /// <typeparam name="TTemplateModel">тип на темплейт модела</typeparam>
        /// <typeparam name="TProperty">тип на свойството</typeparam>
        /// <param name="viewData">хештаблица</param>
        /// <param name="subTemplateExp">път до под-темплейта</param>
        /// <returns></returns>
        public static object GetViewDataForTemplate<TTemplateModel, TProperty>(this ViewDataDictionary<TTemplateModel> viewData, Expression<Func<TTemplateModel, TProperty>> subTemplateExp)
        {
            string name = ExpressionHelper.GetExpressionText(subTemplateExp);
            name = viewData.TemplateInfo.GetFullHtmlFieldName(name);

            return viewData["__" + name];
        }

        /// <summary>
        /// Връща данни от хештаблица
        /// </summary>
        /// <param name="viewData">хештаблица</param>
        /// <param name="input">индекс</param>
        /// <returns></returns>
        public static object GetViewDataForTemplate(this ViewDataDictionary viewData, string input)
        {
            return viewData["__" + input];
        }

        #endregion

        #region Localization

        public static string LocalResources(this WebViewPage page, string key)
        {
            return page.ViewContext.HttpContext.GetLocalResourceObject(page.VirtualPath, key) as string;
        }

        public class Language
        {
            public string Url { get; set; }
            public string ActionName { get; set; }
            public string ControllerName { get; set; }
            public RouteValueDictionary RouteValues { get; set; }
            public bool IsSelected { get; set; }

            public MvcHtmlString HtmlSafeUrl
            {
                get
                {
                    return MvcHtmlString.Create(Url);
                }
            }
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

        #region CheckIsModelPropertyValid

        public static bool IsPropertyValid<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            name = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            return !(htmlHelper.ViewContext.ViewData.ModelState.Keys.Contains(name)
                    && htmlHelper.ViewContext.ViewData.ModelState[name].Errors.Any());
        }

        public static bool IsNonModelFieldValid<TModel>(this HtmlHelper<TModel> htmlHelper, string inputName)
        {
            return !(htmlHelper.ViewContext.ViewData.ModelState.Keys.Contains(inputName)
                    && htmlHelper.ViewContext.ViewData.ModelState[inputName].Errors.Any());
        }

        #endregion
    }
}
