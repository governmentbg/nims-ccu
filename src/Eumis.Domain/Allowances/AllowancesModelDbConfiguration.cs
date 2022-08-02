using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.Allowances
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
