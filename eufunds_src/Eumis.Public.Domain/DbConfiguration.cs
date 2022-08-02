using System.Data.Entity;
using Eumis.Public.Domain.Entities;

namespace Eumis.Public.Domain
{
    public class DbConfiguration : Eumis.Public.Common.Db.IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MapRegionMap());
            modelBuilder.Configurations.Add(new MapMap());
            modelBuilder.Configurations.Add(new MapTypeMap());
            modelBuilder.Configurations.Add(new OpStatOverrideMap());
        }
    }
}
