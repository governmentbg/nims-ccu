using Eumis.Common.Config;
using Eumis.Common.NLog;
using Microsoft.Owin;
using NLog;
using Owin;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(Eumis.Portal.Web.Startup))]
namespace Eumis.Portal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string logsConnectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString.ExpandEnv();
            LogManager.Configuration.SetDatabaseTargetConnectionString("_database", logsConnectionString);

            ConfigureAuth(app);
        }
    }
}
