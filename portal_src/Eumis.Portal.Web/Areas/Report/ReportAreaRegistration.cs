using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eumis.Portal.Web.Areas.Report
{
    public class ReportAreaRegistration : AreaRegistration 
    {
        public const string Name = "Report";
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
                "Report_selected_contract",
                "Report/{session}/{cgid}/{controller}/{action}",
                new { controller = "Home", action = "Index", session = "s", cgid = UrlParameter.Optional },
                constraints: new { cgid = new System.Web.Mvc.Routing.Constraints.GuidRouteConstraint() },
                namespaces: new string[] { "Eumis.Portal.Web.Areas.Report.Controllers" }
            );

            context.MapRoute(
                "Report_default",
                "Report/{session}/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", session = "s", id = UrlParameter.Optional },
                namespaces: new string[] { "Eumis.Portal.Web.Areas.Report.Controllers" }
            );
        }
    }

    public class GuidConstraint : IRouteConstraint
    {

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(parameterName))
            {
                string stringValue = values[parameterName] as string;

                if (!string.IsNullOrEmpty(stringValue))
                {
                    Guid guidValue;

                    return Guid.TryParse(stringValue, out guidValue) && (guidValue != Guid.Empty);
                }
            }

            return false;
        }
    }
}