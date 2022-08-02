using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.Irregularities
{
    public class IrregularityModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IrregularityFinancialStatusMap());
            modelBuilder.Configurations.Add(new IrregularityCategoryMap());
            modelBuilder.Configurations.Add(new IrregularityTypeMap());
            modelBuilder.Configurations.Add(new IrregularitySanctionCategoryMap());
            modelBuilder.Configurations.Add(new IrregularitySanctionTypeMap());
            modelBuilder.Configurations.Add(new IrregularitySignalMap());
            modelBuilder.Configurations.Add(new IrregularitySignalDocMap());
            modelBuilder.Configurations.Add(new IrregularitySignalInvolvedPersonMap());
            modelBuilder.Configurations.Add(new IrregularityMap());
            modelBuilder.Configurations.Add(new IrregularityFinancialCorrectionMap());
            modelBuilder.Configurations.Add(new IrregularityDocMap());
            modelBuilder.Configurations.Add(new IrregularityVersionMap());
            modelBuilder.Configurations.Add(new IrregularityImpairedRegulationMap());
            modelBuilder.Configurations.Add(new IrregularitySanctionMap());
            modelBuilder.Configurations.Add(new IrregularityVersionDocMap());
            modelBuilder.Configurations.Add(new IrregularityVersionInvolvedPersonMap());
        }
    }
}
