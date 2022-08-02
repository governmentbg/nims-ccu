using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using Eumis.Common.Validation;

namespace Eumis.Common.Extensions
{
    //NEEDED BECAUSE OF BUG IN GetFullHtmlFieldId
    /// <summary>
    /// Фиксатор на идентифкатори
    /// </summary>
    public static class IdFixer
    {
        /// <summary>
        /// Връща javascript масив
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="values">стойности</param>
        /// <param name="varName">име на масива</param>
        /// <returns></returns>
        public static string JavaScriptArray(this HtmlHelper htmlHelper, IList<string> values, string varName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("var {0} = {1};", varName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(values));

            return sb.ToString();
        }

        public static string JavaScriptDictionary(this HtmlHelper htmlHelper, IDictionary<string, string> values, string varName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("var {0} = {1};", varName, new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(values));

            return sb.ToString();
        }

        /// <summary>
        /// Фиксатор
        /// </summary>
        /// <param name="originalId">оригинален идентификатор</param>
        /// <returns>фикснат идентификатор</returns>
        public static string FixId(string originalId)
        {
            return CreateSanitizedId(originalId, HtmlHelper.IdAttributeDotReplacement);
        }

        //IDENTICAL WITH System.Web.Mvc.TagBuilder.CreateSanitizedId
        /// <summary>
        /// Създава санитарен идентификатор
        /// </summary>
        /// <param name="originalId">оригинален идентификатор</param>
        /// <param name="dotReplacement">точка заместител</param>
        /// <returns>фикснат идентификатор</returns>
        private static string CreateSanitizedId(string originalId, string dotReplacement)
        {
            if (String.IsNullOrEmpty(originalId))
            {
                return null;
            }

            char firstChar = originalId[0];
            if (!Html401IdUtil.IsLetter(firstChar))
            {
                // the first character must be a letter
                return null;
            }

            StringBuilder sb = new StringBuilder(originalId.Length);
            sb.Append(firstChar);

            for (int i = 1; i < originalId.Length; i++)
            {
                char thisChar = originalId[i];
                if (Html401IdUtil.IsValidIdCharacter(thisChar))
                {
                    sb.Append(thisChar);
                }
                else
                {
                    sb.Append(dotReplacement);
                }
            }

            return sb.ToString();
        }

        //IDENTICAL WITH System.Web.Mvc.TagBuilder.Html401IdUtil
        private static class Html401IdUtil
        {
            private static bool IsAllowableSpecialCharacter(char c)
            {
                switch (c)
                {
                    case '-':
                    case '_':
                    case ':':
                        // note that we're specifically excluding the '.' character
                        return true;

                    default:
                        return false;
                }
            }

            private static bool IsDigit(char c)
            {
                return ('0' <= c && c <= '9');
            }

            public static bool IsLetter(char c)
            {
                return (('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z'));
            }

            public static bool IsValidIdCharacter(char c)
            {
                return (IsLetter(c) || IsDigit(c) || IsAllowableSpecialCharacter(c));
            }
        }
    }
    /// <summary>
    /// HTML Екстенжъни
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Създава идентификатор
        /// </summary>
        /// <param name="htmlHelper">текущия модул</param>
        /// <returns>идентификатор</returns>
        public static string GetUniqueEditorId(this HtmlHelper htmlHelper)
        {
            return Guid.NewGuid().ToString();
        }

        #region StopOnValidationErrors text

        public static string StopOnValidationErrors(this HtmlHelper htmlHelper)
        {
            TagBuilder builder = new TagBuilder("div");
            builder.AddCssClass("validation-summary-errors");

            TagBuilder ul = new TagBuilder("ul");
            TagBuilder li = new TagBuilder("li");
            TagBuilder b = new TagBuilder("b");

            b.InnerHtml = "Моля, оправете грешките, за да подадете заявлението";
            li.InnerHtml += b.ToString(TagRenderMode.Normal);
            ul.InnerHtml += li.ToString(TagRenderMode.Normal);
            builder.InnerHtml += ul.ToString(TagRenderMode.Normal);

            return builder.ToString(TagRenderMode.Normal);
        }

        #endregion

        #region CustomValidationSummary

        // ISUN
        public static MvcHtmlString ValidationSummaryForPopover(this HtmlHelper helper, List<ModelValidationResultExtended> localErrors)
        {
            if (localErrors == null)
                localErrors = new List<ModelValidationResultExtended>();

            try
            {
                if (localErrors.Count() > 0)
                {
                    StringBuilder sb = new StringBuilder("{");

                    foreach (var error in localErrors)
                    {
                        sb.AppendFormat((string)"\"{0}\"", (object)error.MemberName).Append(" : ").AppendFormat("\"{0}\"", error.ErrorSimpleMessage).Append(",");
                    }

                    var json = HttpUtility.JavaScriptStringEncode(sb.ToString().Trim(',') + "}");

                    TagBuilder script = new TagBuilder("script");
                    script.Attributes.Add("type", "text/javascript");
                    script.Attributes.Add("language", "javascript");
                    script.InnerHtml = string.Format("var validationSummaryErrors = JSON.parse('{0}');", json);

                    return MvcHtmlString.Create(script.ToString(TagRenderMode.Normal));
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static MvcHtmlString ValidationSummaryForErrors(this HtmlHelper helper, List<ModelValidationResultExtended> localErrors, List<string> remoteErrors, string validationSummaryErrorTitle, string validationSuccessSummaryTitle, string showText, string hideText, bool activeErrors = false)
        {
            if (localErrors != null || remoteErrors != null)
            {
                if(localErrors == null) localErrors = new List<ModelValidationResultExtended>();
                if(remoteErrors == null) remoteErrors = new List<string>();

                try
                {
                    var errors = localErrors.Select(e => e.DisplayErrorMessage).Concat(remoteErrors);

                    if (errors.Count() > 0)
                    {
                        TagBuilder tagBuilder = new TagBuilder("div");
                        tagBuilder.AddCssClass("validation-summary-errors");

                        TagBuilder divTable = new TagBuilder("div");

                        TagBuilder divTitle = new TagBuilder("div");
                        divTitle.AddCssClass("td vert-top");

                        TagBuilder divLink = new TagBuilder("div");
                        divLink.AddCssClass("td vert-top align-right");

                        TagBuilder h4 = new TagBuilder("h4");
                        h4.AddCssClass("validation-title");
                        h4.InnerHtml = validationSummaryErrorTitle;
                        divTitle.InnerHtml += h4.ToString();

                        TagBuilder showSpan = new TagBuilder("span");
                        showSpan.AddCssClass("validation-show-errors");
                        showSpan.MergeAttribute("style", "display: none;");
                        showSpan.InnerHtml = " <span>" + showText + "</span>";

                        TagBuilder hideSpan = new TagBuilder("span");
                        hideSpan.AddCssClass("validation-hide-errors");
                        hideSpan.MergeAttribute("style", "display: inline-block;");
                        hideSpan.InnerHtml = " <span>" + hideText + "</span>";

                        divLink.InnerHtml += showSpan.ToString();
                        divLink.InnerHtml += hideSpan.ToString();

                        divTable.InnerHtml += divTitle.ToString();
                        divTable.InnerHtml += divLink.ToString();

                        tagBuilder.InnerHtml += divTable.ToString();

                        StringBuilder ulBuilder = new StringBuilder();

                        ulBuilder.AppendLine("<ul style=\"display: inline-block;\">");

                        if (activeErrors)
                        {
                            foreach (var error in localErrors)
                            {
                                TagBuilder li = new TagBuilder("li");
                                li.AddCssClass("active-validation-error");
                                li.MergeAttribute("data-member-name", error.MemberName);
                                li.SetInnerText(error.DisplayErrorMessage);
                                ulBuilder.Append(li.ToString());
                            }

                            List<string> uniqueErrors = new List<string>();

                            foreach (var error in remoteErrors)
                            {
                                if (!uniqueErrors.Contains(error))
                                {
                                    uniqueErrors.Add(error);

                                    ulBuilder.Append("<li>");
                                    ulBuilder.Append(helper.Encode(error));
                                    ulBuilder.AppendLine("</li>");
                                }
                            }
                        }
                        else
                        {
                            List<string> uniqueErrors = new List<string>();

                            foreach (var error in errors)
                            {
                                if (!uniqueErrors.Contains(error))
                                {
                                    uniqueErrors.Add(error);

                                    ulBuilder.Append("<li>");
                                    ulBuilder.Append(helper.Encode(error));
                                    ulBuilder.AppendLine("</li>");
                                }
                            }
                        }


                        ulBuilder.Append("</ul>");

                        tagBuilder.InnerHtml += ulBuilder.ToString();

                        return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
                    }
                    else
                    {
                        TagBuilder tagBuilder = new TagBuilder("div");
                        tagBuilder.AddCssClass("validation-summary-errors validation-success");

                        StringBuilder ulBuilder = new StringBuilder();
                        ulBuilder.AppendLine("<ul>");
                        ulBuilder.Append("<li>");
                        ulBuilder.Append(validationSuccessSummaryTitle);
                        ulBuilder.AppendLine("</li>");
                        ulBuilder.Append("</ul>");

                        tagBuilder.InnerHtml += ulBuilder.ToString();
                        return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
                    }
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static MvcHtmlString ValidationSummaryForWarnings(this HtmlHelper helper, List<string> remoteWarnings, string validationSummaryErrorTitle, string showText, string hideText)
        {
            try
            {
                if (remoteWarnings != null && remoteWarnings.Count > 0)
                {
                    TagBuilder tagBuilder = new TagBuilder("div");
                    tagBuilder.AddCssClass("validation-summary-errors");
                    tagBuilder.AddCssClass("orange-warnings");

                    TagBuilder divTable = new TagBuilder("div");

                    TagBuilder divTitle = new TagBuilder("div");
                    divTitle.AddCssClass("td vert-top");

                    TagBuilder divLink = new TagBuilder("div");
                    divLink.AddCssClass("td vert-top align-right");

                    TagBuilder h4 = new TagBuilder("h4");
                    h4.AddCssClass("validation-title");
                    h4.InnerHtml = validationSummaryErrorTitle;
                    divTitle.InnerHtml += h4.ToString();

                    TagBuilder showSpan = new TagBuilder("span");
                    showSpan.AddCssClass("validation-show-errors");
                    showSpan.MergeAttribute("style", "display: none;");
                    showSpan.InnerHtml = " <span>" + showText + "</span>";

                    TagBuilder hideSpan = new TagBuilder("span");
                    hideSpan.AddCssClass("validation-hide-errors");
                    hideSpan.MergeAttribute("style", "display: inline-block;");
                    hideSpan.InnerHtml = " <span>" + hideText + "</span>";

                    divLink.InnerHtml += showSpan.ToString();
                    divLink.InnerHtml += hideSpan.ToString();

                    divTable.InnerHtml += divTitle.ToString();
                    divTable.InnerHtml += divLink.ToString();

                    tagBuilder.InnerHtml += divTable.ToString();

                    StringBuilder ulBuilder = new StringBuilder();

                    ulBuilder.AppendLine("<ul style=\"display: inline-block;\">");

                    List<string> uniqueErrors = new List<string>();

                    foreach (var error in remoteWarnings)
                    {
                        if (!uniqueErrors.Contains(error))
                        {
                            uniqueErrors.Add(error);

                            ulBuilder.Append("<li>");
                            ulBuilder.Append(helper.Encode(error));
                            ulBuilder.AppendLine("</li>");
                        }
                    }

                    ulBuilder.Append("</ul>");

                    tagBuilder.InnerHtml += ulBuilder.ToString();

                    return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static MvcHtmlString ValidationSummaryForErrorsWithBackButton(this HtmlHelper helper, List<ModelValidationResultExtended> localErrors, List<string> remoteErrors, string validationSummaryErrorTitle, string href, string hrefTitle)
        {
            if (localErrors == null)
                localErrors = new List<ModelValidationResultExtended>();

            if (remoteErrors == null)
                remoteErrors = new List<string>();

            try
            {
                var errors = localErrors.Select(e => e.ErrorComplexMessage).Concat(remoteErrors);

                if (errors.Count() > 0)
                {
                    TagBuilder validator = new TagBuilder("div");
                    validator.AddCssClass("validation-summary-errors");

                    TagBuilder h4 = new TagBuilder("h4");
                    h4.AddCssClass("validation-title");
                    h4.InnerHtml = validationSummaryErrorTitle;
                    validator.InnerHtml += h4.ToString();

                    TagBuilder a = new TagBuilder("a");
                    a.AddCssClass("blue-button small back");
                    a.MergeAttribute("href", href);
                    a.InnerHtml = hrefTitle;

                    validator.InnerHtml = h4.ToString();
                    validator.InnerHtml += a.ToString();

                    return MvcHtmlString.Create(validator.ToString(TagRenderMode.Normal));
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static MvcHtmlString ValidationSummaryForWarningsWithBackButton(this HtmlHelper helper, List<string> remoteWarnings, string validationSummaryErrorTitle, string href, string hrefTitle)
        {
            if (remoteWarnings == null)
                remoteWarnings = new List<string>();

            try
            {
                if (remoteWarnings != null && remoteWarnings.Count > 0)
                {
                    TagBuilder validator = new TagBuilder("div");
                    validator.AddCssClass("validation-summary-errors");
                    validator.AddCssClass("orange-warnings");

                    TagBuilder h4 = new TagBuilder("h4");
                    h4.AddCssClass("validation-title");
                    h4.InnerHtml = validationSummaryErrorTitle;
                    validator.InnerHtml += h4.ToString();

                    TagBuilder a = new TagBuilder("a");
                    a.AddCssClass("blue-button small back");
                    a.MergeAttribute("href", href);
                    a.InnerHtml = hrefTitle;

                    validator.InnerHtml = h4.ToString();
                    validator.InnerHtml += a.ToString();

                    return MvcHtmlString.Create(validator.ToString(TagRenderMode.Normal));
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        //*******************************

        //public static MvcHtmlString ValidationSummaryShowStopErrors(this HtmlHelper helper, string validationSummaryErrorTitle)
        //{
        //    try
        //    {
        //        ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => helper.ViewData.Model, helper.ViewData.Model.GetType());
        //        IEnumerable<ModelValidationResult> errors = ModelValidator.GetModelValidator(metadata, helper.ViewContext.Controller.ControllerContext)
        //            .Validate(null)
        //            .Where(e => ((ModelValidationResultExtended)e).IsStopError);

        //        if (errors != null && errors.Count() > 0)
        //        {
        //            TagBuilder div = new TagBuilder("div");
        //            div.AddCssClass("validation-summary-errors");
        //            div.Attributes.Add("style", "text-align:center;");

        //            StringBuilder builder = new StringBuilder();

        //            div.InnerHtml += "<br>";

        //            TagBuilder b = new TagBuilder("b");
        //            b.InnerHtml = validationSummaryErrorTitle;
        //            div.InnerHtml += b.ToString(TagRenderMode.Normal);
        //            builder.Append("<br>");

        //            builder.AppendLine("<ul>");

        //            foreach (ModelValidationResult validationResult in errors)
        //            {
        //                builder.Append("<li>");
        //                builder.Append((string)helper.Encode(validationResult.Message));
        //                builder.AppendLine("</li>");
        //            }
        //            builder.Append("</ul>");

        //            div.InnerHtml += builder.ToString();

        //            return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        //        }
        //        else
        //            return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public static MvcHtmlString ValidationSummaryHideStopErrors(this HtmlHelper helper)
        //{
        //    try
        //    {
        //        ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => helper.ViewData.Model, helper.ViewData.Model.GetType());
        //        IEnumerable<ModelValidationResult> errors = ModelValidator.GetModelValidator(metadata, helper.ViewContext.Controller.ControllerContext)
        //            .Validate(null)
        //            .Where(e => !((ModelValidationResultExtended)e).IsStopError);

        //        if (errors != null && errors.Count() > 0)
        //        {
        //            TagBuilder tagBuilder = new TagBuilder("div");
        //            tagBuilder.AddCssClass("validation-summary-errors");

        //            StringBuilder builder = new StringBuilder();

        //            builder.AppendLine("<ul>");

        //            foreach (ModelValidationResult validationResult in errors)
        //            {
        //                builder.Append("<li>");
        //                builder.Append((string)helper.Encode(validationResult.Message));
        //                builder.AppendLine("</li>");
        //            }
        //            builder.Append("</ul>");

        //            tagBuilder.InnerHtml = builder.ToString();

        //            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        //        }
        //        else
        //            return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public static MvcHtmlString ValidationSummaryWithPopoverAndMessage(this HtmlHelper helper, string validationSummaryErrorTitle)
        //{
        //    try
        //    {
        //        if (!helper.ViewData.ModelState.IsValid)
        //        {
        //            if (helper.ViewData.ModelState.Count() > 0)
        //            {
        //                TagBuilder div = new TagBuilder("div");
        //                div.AddCssClass("validation-summary-errors");

        //                StringBuilder builder = new StringBuilder("<ul>");
        //                builder.Append("<li>");
        //                builder.Append(validationSummaryErrorTitle);
        //                builder.Append("</li>");
        //                builder.AppendLine("</ul>");

        //                div.InnerHtml += builder.ToString();

        //                StringBuilder sb = new StringBuilder("{");

        //                foreach (var key in helper.ViewData.ModelState.Keys)
        //                {
        //                    if (helper.ViewData.ModelState[key].Errors.Count() > 0)
        //                    {
        //                        var error = Enumerable.Aggregate<string>(helper.ViewData.ModelState[key].Errors.Select(e => e.ErrorMessage), (current, next) => current + ", " + next);

        //                        sb.AppendFormat((string)"\"{0}\"", (object)key).Append(" : ").AppendFormat("\"{0}\"", error).Append(",");
        //                    }
        //                }

        //                var json = HttpUtility.JavaScriptStringEncode(sb.ToString().Trim(',') + "}");

        //                TagBuilder script = new TagBuilder("script");
        //                script.Attributes.Add("type", "text/javascript");
        //                script.Attributes.Add("language", "javascript");
        //                script.InnerHtml = string.Format("var validationSummaryErrors = JSON.parse('{0}');", json);

        //                return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal) + script.ToString(TagRenderMode.Normal));
        //            }
        //        }

        //        return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public static MvcHtmlString ValidationSummaryShowHide(this HtmlHelper helper, string validationSummaryErrorTitle, string showText, string hideText)
        //{
        //    try
        //    {
        //        if (!helper.ViewData.ModelState.IsValid)
        //        {
        //            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => helper.ViewData.Model, helper.ViewData.Model.GetType());
        //            IEnumerable<ModelValidationResult> errors = ModelValidator.GetModelValidator(metadata, helper.ViewContext.Controller.ControllerContext)
        //                .Validate(null);

        //            if (errors.Count() > 0)
        //            {
        //                TagBuilder tagBuilder = new TagBuilder("div");
        //                tagBuilder.AddCssClass("validation-summary-errors");

        //                TagBuilder divTable = new TagBuilder("div");

        //                TagBuilder divTitle = new TagBuilder("div");
        //                divTitle.AddCssClass("td vert-top");

        //                TagBuilder divLink = new TagBuilder("div");
        //                divLink.AddCssClass("td vert-top align-right");

        //                TagBuilder h4 = new TagBuilder("h4");
        //                h4.AddCssClass("validation-title");
        //                h4.InnerHtml = validationSummaryErrorTitle;
        //                divTitle.InnerHtml += h4.ToString();

        //                TagBuilder showSpan = new TagBuilder("span");
        //                showSpan.AddCssClass("validation-show-errors expand-all-section");
        //                showSpan.MergeAttribute("style", "display: inline-block;");
        //                showSpan.InnerHtml = " <span>" + showText + "</span>";

        //                TagBuilder hideSpan = new TagBuilder("span");
        //                hideSpan.AddCssClass("validation-hide-errors");
        //                hideSpan.MergeAttribute("style", "display: none;");
        //                hideSpan.InnerHtml = " <span>" + hideText + "</span>";

        //                divLink.InnerHtml += showSpan.ToString();
        //                divLink.InnerHtml += hideSpan.ToString();

        //                divTable.InnerHtml += divTitle.ToString();
        //                divTable.InnerHtml += divLink.ToString();

        //                tagBuilder.InnerHtml += divTable.ToString();

        //                StringBuilder ulBuilder = new StringBuilder();

        //                ulBuilder.AppendLine("<ul style=\"display: none;\">");

        //                List<string> uniqueErrors = new List<string>();

        //                foreach (var error in errors)
        //                {
        //                    if (error is ModelValidationResultExtended)
        //                    {
        //                        var extended = (ModelValidationResultExtended)error;

        //                        if (!uniqueErrors.Contains(extended.ErrorComplexMessage))
        //                        {
        //                            uniqueErrors.Add(extended.ErrorComplexMessage);

        //                            ulBuilder.Append("<li>");
        //                            ulBuilder.Append(helper.Encode(extended.ErrorComplexMessage));
        //                            ulBuilder.AppendLine("</li>");
        //                        }
        //                    }
        //                }

        //                ulBuilder.Append("</ul>");

        //                tagBuilder.InnerHtml += ulBuilder.ToString();

        //                return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        //            }
        //        }

        //        return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public static bool HasStopErrors(this HtmlHelper helper)
        //{
        //    try
        //    {
        //        ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => helper.ViewData.Model, helper.ViewData.Model.GetType());
        //        return ModelValidator
        //            .GetModelValidator(metadata, helper.ViewContext.Controller.ControllerContext)
        //            .Validate(null)
        //            .Any(e => ((ModelValidationResultExtended)e).IsStopError);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        #endregion

        #region IsOnceValidated hidden field

        /// <summary>
        /// Скрито поле, маркиращо дали модела е бил валидиран
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на полето</param>
        /// <param name="value">стойност на полето</param>
        /// <returns></returns>
        public static MvcHtmlString IsOnceValidated(this HtmlHelper htmlHelper, string name, object value)
        {
            return MvcHtmlString.Create(IsOnceValidatedInternal(htmlHelper, name, value, null));
        }

        /// <summary>
        /// Скрито поле, маркиращо дали модела е бил валидиран
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">име на полето</param>
        /// <param name="value">стойност на полето</param>
        /// <param name="htmlAttributes">атрибути към полето</param>
        /// <returns></returns>
        public static string IsOnceValidated(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return IsOnceValidatedInternal(htmlHelper, name, value, htmlAttributes);
        }

        private static string IsOnceValidatedInternal(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", "hidden", true);
            tagBuilder.MergeAttribute("name", name, true);
            tagBuilder.MergeAttribute("value", value.ToString(), true);

            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }

        #endregion

        #region AutoCompleteTextBoxFor

        // This overload is used with the T4MVC and does not compile without it
        // you can remove it if you are not using T4MVC
        /// <summary>
        /// Автоматична допълваща контрола
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <typeparam name="TName">име</typeparam>
        /// <param name="htmlHelper">текущ модул</param>
        /// <param name="codeFieldExpression">полето код</param>
        /// <param name="nameFieldExpression">полето име</param>
        /// <param name="result">резултат</param>
        /// <param name="parentFieldExpression">полето контейнер</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <param name="additionalOptions">допълнителни настройки</param>
        /// <returns>контрола</returns>
        public static MvcHtmlString AutoCompleteTextBoxFor<TModel, TCode, TName>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            ActionResult result, Expression<Func<TModel, object>> parentFieldExpression = null,
            object htmlAttributes = null, object additionalOptions = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string url = urlHelper.RouteUrl(result.GetRouteValueDictionary());

            return AutoCompleteTextBoxHelper(htmlHelper, codeFieldExpression, nameFieldExpression, parentFieldExpression, url,
                new RouteValueDictionary(htmlAttributes), new RouteValueDictionary(additionalOptions));
        }
        /// <summary>
        /// Автоматична допълваща контрола
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <typeparam name="TName">име</typeparam>
        /// <param name="htmlHelper">текущ модул</param>
        /// <param name="codeFieldExpression">полето код</param>
        /// <param name="nameFieldExpression">полето име</param>
        /// <param name="actionName">action</param>
        /// <param name="result">резултат</param>
        /// <param name="parentFieldExpression">полето контейнер</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <param name="additionalOptions">допълнителни настройки</param>
        /// <returns>контрола</returns>
        public static MvcHtmlString AutoCompleteTextBoxFor<TModel, TCode, TName>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            string actionName, string controllerName = null, object routeValues = null,
            Expression<Func<TModel, object>> parentFieldExpression = null, object htmlAttributes = null,
            object additionalOptions = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string url = urlHelper.Action(actionName, controllerName, routeValues);

            return AutoCompleteTextBoxHelper(htmlHelper, codeFieldExpression, nameFieldExpression, parentFieldExpression, url,
                new RouteValueDictionary(htmlAttributes), new RouteValueDictionary(additionalOptions));
        }
        /// <summary>
        /// Автоматична допълваща контрола
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <typeparam name="TName">име</typeparam>
        /// <param name="htmlHelper">текущ модул</param>
        /// <param name="codeFieldExpression">полето код</param>
        /// <param name="nameFieldExpression">полето име</param>
        /// <param name="result">резултат</param>
        /// <param name="parentFieldExpression">полето контейнер</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <param name="additionalOptions">допълнителни настройки</param>
        /// <returns>контрола</returns>
        private static MvcHtmlString AutoCompleteTextBoxHelper<TModel, TCode, TName>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            Expression<Func<TModel, object>> parentFieldExpression,
            string url, IDictionary<string, object> htmlAttributes, IDictionary<string, object> additionalOptions)
        {
            string nameFieldExpr = ExpressionHelper.GetExpressionText(nameFieldExpression);
            string nameFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(nameFieldExpr));

            object nameFieldValue = ModelMetadata.FromLambdaExpression(nameFieldExpression, htmlHelper.ViewData).Model;

            string codeFieldExpr = ExpressionHelper.GetExpressionText(codeFieldExpression);
            string codeFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(codeFieldExpr));
            object codeFieldValue = ModelMetadata.FromLambdaExpression(codeFieldExpression, htmlHelper.ViewData).Model;

            string textBoxExpr = nameFieldExpr + "TextBox";
            string textBoxId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(textBoxExpr));

            string parentFieldExpr = parentFieldExpression != null ? ExpressionHelper.GetExpressionText(parentFieldExpression) : null;
            string parentFieldId = parentFieldExpr != null ? IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(parentFieldExpr)) : null;

            string[] optionsArray = additionalOptions
                .Select(kvp => String.Format("{0}:{1}", kvp.Key, kvp.Value))
                .ToArray();

            string initScript = String.Format(
            "<script type=\"text/javascript\">\n" +
            "    createAutoComplete({0}, {1}, {2}, {3}, {4}, {{{5}}});\n" +
            "</script>", ToJsString(url), ToJsString(textBoxId), ToJsString(codeFieldId),
                ToJsString(nameFieldId), ToJsString(parentFieldId), String.Join(",", optionsArray));

            string textBoxElement = htmlHelper.TextBox(textBoxExpr, nameFieldValue, htmlAttributes).ToHtmlString();
            string hiddenValueElement = htmlHelper.Hidden(nameFieldExpr, nameFieldValue).ToHtmlString();
            string hiddenIdElement = htmlHelper.Hidden(codeFieldExpr, codeFieldValue).ToHtmlString();

            return MvcHtmlString.Create(String.Format("{0}\n{1}\n{2}\n{3}", hiddenIdElement, hiddenValueElement, textBoxElement, initScript));
        }

        #endregion

        #region CascadingDropDownFor

        // This overload is used with the T4MVC and does not compile without it
        // you can remove it if you are not using T4MVC
        /// <summary>
        /// Автоматична допълваща контрола
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <typeparam name="TName">име</typeparam>
        /// <typeparam name="TParent">ралативност</typeparam>
        /// <param name="htmlHelper">текущ модул</param>
        /// <param name="codeFieldExpression">полето код</param>
        /// <param name="nameFieldExpression">полето име</param>
        /// <param name="parentFieldExpression">полето релативност</param>
        /// <param name="initValuesFunc">източник на данните</param>
        /// <param name="result">резулатат</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <param name="additionalOptions">допълнителни настройки</param>
        /// <returns>контрола</returns>
        public static MvcHtmlString CascadingDropDownFor<TModel, TCode, TName, TParent>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            Expression<Func<TModel, TParent>> parentFieldExpression,
            Func<TParent, IEnumerable<SerializableSelectListItem>> initValuesFunc, ActionResult result, object htmlAttributes = null,
            object additionalOptions = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string url = urlHelper.RouteUrl(result.GetRouteValueDictionary());

            return CascadingDropDownForHelper(htmlHelper, codeFieldExpression, nameFieldExpression, parentFieldExpression, initValuesFunc,
                url, new RouteValueDictionary(htmlAttributes), new RouteValueDictionary(additionalOptions));
        }

        /// <summary>
        /// Автоматична допълваща контрола
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <typeparam name="TName">име</typeparam>
        /// <typeparam name="TParent">ралативност</typeparam>
        /// <param name="htmlHelper">текущ модул</param>
        /// <param name="codeFieldExpression">полето код</param>
        /// <param name="nameFieldExpression">полето име</param>
        /// <param name="parentFieldExpression">полето релативност</param>
        /// <param name="initValuesFunc">източник на данните</param>
        /// <param name="actionName">име на страницата</param>
        /// <param name="controllerName">име на конторлата</param>
        /// <param name="routeValues">допълнителни прикачени данни</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <param name="additionalOptions">допълнителни настройки</param>
        /// <returns>контрола</returns>
        public static MvcHtmlString CascadingDropDownFor<TModel, TCode, TName, TParent>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            Expression<Func<TModel, TParent>> parentFieldExpression,
            Func<TParent, IEnumerable<SerializableSelectListItem>> initValuesFunc, string actionName, string controllerName = null,
            object routeValues = null, object htmlAttributes = null, object additionalOptions = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string url = urlHelper.Action(actionName, controllerName, routeValues);

            return CascadingDropDownForHelper(htmlHelper, codeFieldExpression, nameFieldExpression, parentFieldExpression, initValuesFunc,
                url, new RouteValueDictionary(htmlAttributes), new RouteValueDictionary(additionalOptions));
        }

        /// <summary>
        /// Автоматична допълваща контрола
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <typeparam name="TName">име</typeparam>
        /// <typeparam name="TParent">ралативност</typeparam>
        /// <param name="htmlHelper">текущ модул</param>
        /// <param name="codeFieldExpression">полето код</param>
        /// <param name="nameFieldExpression">полето име</param>
        /// <param name="parentFieldExpression">полето релативност</param>
        /// <param name="initValuesFunc">източник на данните</param>
        /// <param name="url">връзка</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <param name="additionalOptions">допълнителни настройки</param>
        /// <returns>контрола</returns>
        private static MvcHtmlString CascadingDropDownForHelper<TModel, TCode, TName, TParent>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            Expression<Func<TModel, TParent>> parentFieldExpression,
            Func<TParent, IEnumerable<SerializableSelectListItem>> initValuesFunc, string url, IDictionary<string, object> htmlAttributes,
            IDictionary<string, object> additionalOptions)
        {
            string nameFieldExpr = ExpressionHelper.GetExpressionText(nameFieldExpression);
            string nameFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(nameFieldExpr));
            object nameFieldValue = ModelMetadata.FromLambdaExpression(nameFieldExpression, htmlHelper.ViewData).Model;

            string codeFieldExpr = ExpressionHelper.GetExpressionText(codeFieldExpression);
            string codeFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(codeFieldExpr));
            object codeFieldValue = ModelMetadata.FromLambdaExpression(codeFieldExpression, htmlHelper.ViewData).Model;

            string parentFieldExpr = ExpressionHelper.GetExpressionText(parentFieldExpression);
            string parentFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(parentFieldExpr));
            object parentFieldValue = ModelMetadata.FromLambdaExpression(parentFieldExpression, htmlHelper.ViewData).Model;

            List<SerializableSelectListItem> initialItems = new List<SerializableSelectListItem>();

            if (parentFieldValue != null)
            {
                initialItems.AddRange(initValuesFunc((TParent)parentFieldValue));

                if (codeFieldValue != null)
                {
                    foreach (var item in initialItems)
                    {
                        if (item.Value == codeFieldValue.ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            additionalOptions.Add("loadingText", "'" + UITextConstants.CascadingDropDownLoadingText + "'");
            additionalOptions.Add("errorText", "'" + UITextConstants.CascadingDropDownErrorText + "'");
            additionalOptions.Add("parameterName", "'" + UITextConstants.CascadingDropDownParameterName + "'");
            additionalOptions.Add("disabledClass", "'" + UITextConstants.CascadingDropDownDisabledCssClassName + "'");

            string[] optionsArray = additionalOptions
                .Select(kvp => String.Format("{0}:{1}", kvp.Key, kvp.Value))
                .ToArray();

            string initScript = String.Format(
            "<script type=\"text/javascript\">\n" +
            "    createCascadingDropDown('{0}', '{1}', '{2}', '{3}', {{{4}}});\n" +
            "</script>", codeFieldId, nameFieldId, parentFieldId, url, String.Join(",", optionsArray));

            var ddl = initialItems.Select(e => new SelectListItem {Selected = e.Selected, Value = e.Value, Text = e.Text});
            string dropDownElement = htmlHelper.DropDownList(codeFieldExpr, ddl, htmlAttributes).ToHtmlString();
            string hiddenElement = htmlHelper.Hidden(nameFieldExpr, nameFieldValue).ToHtmlString();

            return MvcHtmlString.Create(String.Format("{0}\n{1}\n{2}", hiddenElement, dropDownElement, initScript));
        }

        #endregion

        #region DropDownListWithHiddenFor
        /// <summary>
        /// Падащ списък със скрито поле за име
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <typeparam name="TName">име</typeparam>
        /// <param name="htmlHelper">текущ модел</param>
        /// <param name="codeFieldExpression">плето код</param>
        /// <param name="nameFieldExpression">полето име</param>
        /// <param name="initValues">източник на данни</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <returns>контрола</returns>
        public static MvcHtmlString DropDownListWithHiddenFor<TModel, TCode, TName>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            IEnumerable<SerializableSelectListItem> initValues, object htmlAttributes = null, bool hasEmpty = true)
        {
            return DropDownListWithHiddenFor(htmlHelper, codeFieldExpression, nameFieldExpression, initValues, new RouteValueDictionary(htmlAttributes), hasEmpty);
        }
        /// <summary>
        /// Падащ списък със скрито поле за име
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <typeparam name="TName">име</typeparam>
        /// <param name="htmlHelper">текущ модел</param>
        /// <param name="codeFieldExpression">плето код</param>
        /// <param name="nameFieldExpression">полето име</param>
        /// <param name="initValues">източник на данни</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <returns>контрола</returns>
        private static MvcHtmlString DropDownListWithHiddenFor<TModel, TCode, TName>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            IEnumerable<SerializableSelectListItem> initValues, IDictionary<string, object> htmlAttributes, bool hasEmpty)
        {
            string nameFieldExpr = ExpressionHelper.GetExpressionText(nameFieldExpression);
            string nameFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(nameFieldExpr));
            object nameFieldValue = ModelMetadata.FromLambdaExpression(nameFieldExpression, htmlHelper.ViewData).Model;

            string codeFieldExpr = ExpressionHelper.GetExpressionText(codeFieldExpression);
            string codeFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(codeFieldExpr));
            object codeFieldValue = ModelMetadata.FromLambdaExpression(codeFieldExpression, htmlHelper.ViewData).Model;

            List<SerializableSelectListItem> initialItems = new List<SerializableSelectListItem>();
            initialItems.AddRange(initValues);

            if (codeFieldValue != null)
            {
                string codeFieldValueString = codeFieldValue.ToString();

                foreach (var item in initialItems)
                {
                    if (item.Value == codeFieldValueString)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }

            string initScript = String.Format(
            "<script type=\"text/javascript\">\n" +
            "    bindField('{0}', '{1}');\n" +
            "</script>", codeFieldId, nameFieldId);

            string dropDownElement = string.Empty;

            var ddl = initialItems.Select(e => new SelectListItem { Selected = e.Selected, Value = e.Value, Text = e.Text });
            // if (hasEmpty) dropDownElement = htmlHelper.DropDownList(codeFieldExpr, initialItems, UITextConstants.DropDownEmptyItemText, htmlAttributes).ToHtmlString();
            if (hasEmpty) dropDownElement = htmlHelper.DropDownList(codeFieldExpr, ddl, "", htmlAttributes).ToHtmlString();
            else dropDownElement = htmlHelper.DropDownList(codeFieldExpr, ddl, htmlAttributes).ToHtmlString();

            string hiddenElement = htmlHelper.Hidden(nameFieldExpr, (codeFieldValue == null) ? string.Empty : nameFieldValue).ToHtmlString();

            return MvcHtmlString.Create(String.Format("{0}\n{1}\n{2}", dropDownElement, hiddenElement, initScript));
        }

        /// <summary>
        /// Падащ списък със скрито поле за име
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <param name="htmlHelper">текущ модел</param>
        /// <param name="codeFieldExpression">плето код</param>
        /// <param name="initValues">източник на данни</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <returns>контрола</returns>
        public static MvcHtmlString DropDownListWithHiddenFor<TModel, TCode>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, IEnumerable<SerializableSelectListItem> initValues, object htmlAttributes = null)
        {
            return DropDownListWithHiddenFor(htmlHelper, codeFieldExpression, initValues, new RouteValueDictionary(htmlAttributes));
        }
        /// <summary>
        /// Падащ списък със скрито поле за име
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TCode">код</typeparam>
        /// <param name="htmlHelper">текущ модел</param>
        /// <param name="codeFieldExpression">плето код</param>
        /// <param name="initValues">източник на данни</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <returns>контрола</returns>
        private static MvcHtmlString DropDownListWithHiddenFor<TModel, TCode>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, IEnumerable<SerializableSelectListItem> initValues, IDictionary<string, object> htmlAttributes)
        {
            string codeFieldExpr = ExpressionHelper.GetExpressionText(codeFieldExpression);
            string codeFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(codeFieldExpr));
            object codeFieldValue = ModelMetadata.FromLambdaExpression(codeFieldExpression, htmlHelper.ViewData).Model;

            List<SerializableSelectListItem> initialItems = new List<SerializableSelectListItem>();
            initialItems.AddRange(initValues);

            if (codeFieldValue != null)
            {
                string codeFieldValueString = codeFieldValue.ToString();

                foreach (var item in initialItems)
                {
                    if (item.Value == codeFieldValueString)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }

            var ddl = initialItems.Select(e => new SelectListItem { Selected = e.Selected, Value = e.Value, Text = e.Text });
            string dropDownElement = htmlHelper.DropDownList(codeFieldExpr, ddl, UITextConstants.DropDownEmptyItemText, htmlAttributes).ToHtmlString();

            return MvcHtmlString.Create(String.Format("{0}", dropDownElement));
        }
        #endregion

        #region HiddenWithDefaultValueFor
        /// <summary>
        /// Скрито поле с подразбираща стойност
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TValue">тип стойност</typeparam>
        /// <param name="htmlHelper">текущ модул</param>
        /// <param name="expression">отнасящо се поле</param>
        /// <param name="value">стойност</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <returns>контрола</returns>
        public static MvcHtmlString HiddenWithDefaultValueFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, TValue value, object htmlAttributes = null, bool useDefaultValue = false)
        {
            return HiddenWithDefaultValueFor(htmlHelper, expression, value, new RouteValueDictionary(htmlAttributes), useDefaultValue);
        }
        /// <summary>
        /// Скрито поле с подразбираща стойност
        /// </summary>
        /// <typeparam name="TModel">модел</typeparam>
        /// <typeparam name="TValue">тип стойност</typeparam>
        /// <param name="htmlHelper">текущ модул</param>
        /// <param name="expression">отнасящо се поле</param>
        /// <param name="value">стойност</param>
        /// <param name="htmlAttributes">атрибути</param>
        /// <returns>контрола</returns>
        private static MvcHtmlString HiddenWithDefaultValueFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, TValue value, IDictionary<string, object> htmlAttributes, bool useDefaultValue = false)
        {
            string fieldExpr = ExpressionHelper.GetExpressionText(expression);
            string fieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(fieldExpr));
            object fieldValue = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;

            if (fieldValue == null || useDefaultValue)
                fieldValue = value;

            return htmlHelper.Hidden(fieldExpr, fieldValue);
        }

        #endregion

        #region DropDownListWithAutocompleteFor

        public static MvcHtmlString DropDownListWithAutocompleteFor<TModel, TCode, TName>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            IEnumerable<AutocompleteSelectListItem> initValues, object htmlAttributes = null)
        {
            return DropDownListWithAutocompleteFor(htmlHelper, codeFieldExpression, nameFieldExpression, initValues, new RouteValueDictionary(htmlAttributes));
        }

        private static MvcHtmlString DropDownListWithAutocompleteFor<TModel, TCode, TName>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TCode>> codeFieldExpression, Expression<Func<TModel, TName>> nameFieldExpression,
            IEnumerable<AutocompleteSelectListItem> initValues, IDictionary<string, object> htmlAttributes)
        {
            string nameFieldExpr = ExpressionHelper.GetExpressionText(nameFieldExpression);
            string nameFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(nameFieldExpr));
            object nameFieldValue = ModelMetadata.FromLambdaExpression(nameFieldExpression, htmlHelper.ViewData).Model;

            string codeFieldExpr = ExpressionHelper.GetExpressionText(codeFieldExpression);
            string codeFieldId = IdFixer.FixId(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(codeFieldExpr));
            object codeFieldValue = ModelMetadata.FromLambdaExpression(codeFieldExpression, htmlHelper.ViewData).Model;

            var _javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var initValuesWithEmpty = initValues.ToList();
            initValuesWithEmpty.Insert(0, new AutocompleteSelectListItem() { id = "_clear", text = UITextConstants.DropDownClearItemText });

            string initScript = String.Format(
            "<script type=\"text/javascript\">\n" +
            "    createDropDownListWithAutocomplete('{0}', '{1}', '{2}', '{3}', '{4}');\n" +
            "</script>", codeFieldId, nameFieldId, codeFieldValue, nameFieldValue, HttpUtility.JavaScriptStringEncode(_javaScriptSerializer.Serialize(initValuesWithEmpty)));

            string hiddenElementCode = htmlHelper.Hidden(codeFieldExpr, (codeFieldValue == null) ? string.Empty : codeFieldValue, htmlAttributes).ToHtmlString();
            string hiddenElementName = htmlHelper.Hidden(nameFieldExpr, (nameFieldValue == null) ? string.Empty : nameFieldValue).ToHtmlString();

            return MvcHtmlString.Create(String.Format("{0}\n{1}\n{2}", hiddenElementCode, hiddenElementName, initScript));
        }

        #endregion

        #region Private Utility Methods

        private static string ToJsString(string s)
        {
            return s != null ? "'" + s + "'" : "undefined";
        }

        #endregion

        #region data-edit checkbox with hidden


        public static MvcHtmlString CheckboxWithHiddenDataEdit(this HtmlHelper helper, bool hasHistory, string editorId, string inputName, bool isModTypeUpdate, string editText)
        {
            if (!hasHistory)
                return MvcHtmlString.Create("");

            TagBuilder hidden = new TagBuilder("input");
            hidden.Attributes.Add("type", "hidden");
            hidden.Attributes.Add("name", inputName);
            hidden.Attributes.Add("data-edit", editorId);

            TagBuilder label = new TagBuilder("label");
            label.Attributes.Add("data-edit", editorId);

            TagBuilder checkbox = new TagBuilder("input");
            checkbox.Attributes.Add("type", "checkbox");
            checkbox.Attributes.Add("name", editorId);
            checkbox.Attributes.Add("data-edit", editorId);
            if (isModTypeUpdate)
                checkbox.Attributes.Add("checked", "checked");

            label.InnerHtml = checkbox.ToString(TagRenderMode.Normal) + editText;

            return MvcHtmlString.Create(hidden.ToString(TagRenderMode.Normal) + label.ToString(TagRenderMode.Normal));
        }


        #endregion
    }
}