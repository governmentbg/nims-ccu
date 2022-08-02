using System;
using System.Web.Mvc;

namespace Eumis.Public.Web.InfrastructureClasses
{
    public static class UtilHelpers
    {
        public static string Action(this UrlHelper urlHelper, UrlDef url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (urlHelper == null)
            {
                throw new ArgumentNullException(nameof(urlHelper));
            }

            return urlHelper.Action(url.Action, url.Controller, url.UrlParams);
        }
    }
}