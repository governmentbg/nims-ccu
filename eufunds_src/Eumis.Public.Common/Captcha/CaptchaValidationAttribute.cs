using System;
using System.Linq;
using System.Web.Mvc;

namespace Eumis.Public.Common.Captcha
{
    /// <summary>
    /// CaptchaValidationAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class CaptchaValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaValidationAttribute"/> class.
        /// </summary>
        public CaptchaValidationAttribute()
            : this("captcha")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaValidationAttribute"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        public CaptchaValidationAttribute(string field)
        {
            this.Field = field;
        }

        /// <summary>
        /// Gets the field.
        /// </summary>
        /// <value>The field.</value>
        public string Field { get; private set; }

        /// <summary>
        /// Called when [action executed].
        /// </summary>
        /// <param name="filterContext">The filter filterContext.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var captchaAttribute = Enumerable.FirstOrDefault(filterContext.ActionDescriptor.GetCustomAttributes(typeof(CaptchaValidationAttribute), false)) as CaptchaValidationAttribute;

            if (captchaAttribute != null)
            {
                // make sure no values are getting sent in from the outside
                filterContext.ActionParameters["captchaValid"] = false;

                // get the guid from the post back
                string guid = filterContext.HttpContext.Request.Form["captcha-guid"];

                // check for the guid because it is required from the rest of the opperation
                if (!string.IsNullOrEmpty(guid))
                {
                    // get values
                    CaptchaImage image = CaptchaImage.GetCachedCaptcha(guid);
                    string actualValue = filterContext.HttpContext.Request.Form[captchaAttribute.Field];
                    string expectedValue = image == null ? string.Empty : image.Text;

                    // removes the captch from cache so it cannot be used again
                    filterContext.HttpContext.Cache.Remove(guid);

                    // validate the captcha
                    filterContext.ActionParameters["captchaValid"] =
                            !string.IsNullOrEmpty(actualValue)
                            && !string.IsNullOrEmpty(expectedValue)
                            && string.Equals(actualValue, expectedValue, StringComparison.OrdinalIgnoreCase);
                }
            }
        }
    }
}