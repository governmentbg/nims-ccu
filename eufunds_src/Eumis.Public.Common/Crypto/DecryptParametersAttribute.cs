using System;
using System.Web.Mvc;

namespace Eumis.Public.Common.Crypto
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DecryptParametersAttribute : ActionFilterAttribute
    {
        public string[] IdsParamName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var idParamName in this.IdsParamName)
            {
                if (filterContext.ActionParameters.ContainsKey(idParamName))
                {
                    var idValue = filterContext.ActionParameters[idParamName].ToString();

                    if (!string.IsNullOrEmpty(idValue))
                    {
                        filterContext.ActionParameters[idParamName] = ConfigurationBasedStringEncrypter.Decrypt(idValue);
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}