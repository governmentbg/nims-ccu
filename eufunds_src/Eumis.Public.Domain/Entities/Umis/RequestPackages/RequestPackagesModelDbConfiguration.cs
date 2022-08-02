using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.RequestPackages
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
