using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.Allowances
{
    public class AllowancesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AllowanceMap());
            modelBuilder.Configurations.Add(new AllowanceRateMap());
        }
    }
}
