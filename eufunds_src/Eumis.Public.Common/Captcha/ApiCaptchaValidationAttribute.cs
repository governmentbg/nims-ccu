using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Eumis.Public.Common.Captcha
{
    /// <summary>
    /// ApiCaptchaValidationAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class ApiCaptchaValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCaptchaValidationAttribute"/> class.
        /// </summary>
        public ApiCaptchaValidationAttribute()
            : this("captcha")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCaptchaValidationAttribute"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        public ApiCaptchaValidationAttribute(string field)
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
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var captchaAttribute = Enumerable.FirstOrDefault<object>(filterContext.ActionDescriptor.GetCustomAttributes<ApiCaptchaValidationAttribute>()) as ApiCaptchaValidationAttribute;

            if (captchaAttribute != null)
            {
                string key = filterContext.ActionArguments.Keys.Single();

                ApiCaptchaModel model = filterContext.ActionArguments[key] as ApiCaptchaModel;

                // make sure no values are getting sent in from the outside
                model.CaptchaValid = false;

                // get the guid from the post back
                string guid = model.CaptchaGuid;

                // check for the guid because it is required from the rest of the opperation
                if (!string.IsNullOrEmpty(guid))
                {
                    // get values
                    CaptchaImage image = CaptchaImage.GetCachedCaptcha(guid);
                    string actualValue = model.Captcha;
                    string expectedValue = image == null ? string.Empty : image.Text;

                    // removes the captch from cache so it cannot be used again
                    System.Web.HttpRuntime.Cache.Remove(guid);

                    // validate the captcha
                    model.CaptchaValid =
                            !string.IsNullOrEmpty(actualValue)
                            && !string.IsNullOrEmpty(expectedValue)
                            && string.Equals(actualValue, expectedValue, StringComparison.OrdinalIgnoreCase);
                }
            }
        }
    }
}