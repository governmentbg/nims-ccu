using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.Indicators
{
    public class IndicatorsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IndicatorMap());
            modelBuilder.Configurations.Add(new IndicatorItemTypeMap());
        }
    }
}
