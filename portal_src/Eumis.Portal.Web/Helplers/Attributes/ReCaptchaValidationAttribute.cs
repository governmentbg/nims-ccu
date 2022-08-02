using Eumis.Common.ReCaptcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Helplers.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class ReCaptchaValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaValidationAttribute"/> class.
        /// </summary>
        public ReCaptchaValidationAttribute()
        {
        }

        /// <summary>
        /// Called when [action executed].
        /// </summary>
        /// <param name="filterContext">The filter filterContext.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // make sure no values are getting sent in from the outside
            filterContext.ActionParameters["captchaValid"] = false;

            if (Constants.SkipRecaptchaValidation)
            {
                filterContext.ActionParameters["captchaValid"] = true;
                return;
            }
            
            var captchaAttribute = Enumerable.FirstOrDefault<object>(filterContext.ActionDescriptor.GetCustomAttributes(typeof(ReCaptchaValidationAttribute), false)) as ReCaptchaValidationAttribute;

            if (captchaAttribute != null)
            {
                // get the ReCaptcha-response from the post back
                var gresponse = filterContext.HttpContext.Request.Form["g-recaptcha-response"];
                try
                {
                    var googleResponse = ReCaptchaCommunicator.GetReCaptchaResponse(gresponse, Constants.ReCaptchaServerKey);

                    // validate the captcha
                    filterContext.ActionParameters["captchaValid"] = googleResponse.Success;
                }
                catch
                {
                }
            }
        }

    }
}