using Eumis.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Portal.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            routes.MapRoute(
                name: "LocalizationSession", // Route name
                url: "{lang}/{session}/{controller}/{action}/{id}", // URL with parameters
                defaults: new {lang = SystemLocalization.GetDefaultCulture(), controller = "Public", action = "CurrentNews", session = "s", id = UrlParameter.Optional }, // Parameter defaults
                constraints: new { lang = "^bg$|^en$" },
                namespaces: new string[] { "Eumis.Portal.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Public", action = "CurrentNews", id = UrlParameter.Optional },
                namespaces: new string[] { "Eumis.Portal.Web.Controllers" }
            );
        }
    }
}
