using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.BasicInterestRates
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
