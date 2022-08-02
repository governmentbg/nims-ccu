using Eumis.Common.Db;
using System.Data.Entity;

namespace Eumis.Domain.RequestPackages
{
    public class RequestPackagesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RequestPackageMap());
            modelBuilder.Configurations.Add(new RequestPackageUserMap());
            modelBuilder.Configurations.Add(new RegDataRequestMap());
            modelBuilder.Configurations.Add(new PermissionRequestMap());
        }
    }
}
