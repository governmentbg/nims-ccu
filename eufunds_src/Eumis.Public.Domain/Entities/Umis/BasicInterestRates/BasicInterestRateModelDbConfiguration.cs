using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.BasicInterestRates
{
    public class BasicInterestRateModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BasicInterestRateMap());
            modelBuilder.Configurations.Add(new InterestRateMap());
        }
    }
}
