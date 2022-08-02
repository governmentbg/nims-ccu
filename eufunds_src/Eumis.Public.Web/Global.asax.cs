using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Eumis.Public.Web.App_Start;

namespace Eumis.Public.Web
{
#pragma warning disable SA1649 // SA1649FileNameMustMatchTypeName
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoCConfig.Init()));
        }
    }
#pragma warning restore SA1649 // SA1649FileNameMustMatchTypeName
}
