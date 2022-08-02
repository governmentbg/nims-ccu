using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.InvestmentPriorities
{
    public class InvestmentPrioritiesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new InvestmentPriorityMap());
        }
    }
}
