using Eumis.Common.Config;
using System.Configuration;
using System.Web;

namespace Eumis.Portal.Web
{
    public class SessionPartitionResolver : IPartitionResolver
    {
        private string connectionString;
        public void Initialize()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["PortalSessions"].ConnectionString.ExpandEnv();
        }

        public string ResolvePartition(object key)
        {
            return this.connectionString;
        }
    }
}