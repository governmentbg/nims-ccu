using Eumis.Common.Localization;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Areas.Private
{
    public class PrivateAreaRegistration : AreaRegistration
    {
        public const string Name = "Private";
        public override string AreaName
        {
            get
            {
                return Name;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Private_default_area",
                "{lang}/Private/{session}/{controller}/{action}/{id}",
                new { lang = SystemLocalization.GetDefaultCulture(), action = "Index", id = UrlParameter.Optional },
                constraints: new { lang = "^bg$|^en$" },
                namespaces: new string[] { "Eumis.Portal.Web.Areas.Private.Controllers" }
            );

            context.MapRoute(
                "Private_default",
                "Private/{session}/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Eumis.Portal.Web.Areas.Private.Controllers" }
            );
        }
    }
}