using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Public.Web
{
    public static class RouteConfig
    {
        public const string OPABB = "op";
        public const string PRABB = "pr";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{" + OPABB + "}/{" + PRABB + "}/{controller}/{action}/{id}",
                defaults: new { lang = "bg", op = 0, pr = 0, controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { lang = "^bg$|^en$", op = @"\d+", pr = @"\d+" });
        }
    }
}
