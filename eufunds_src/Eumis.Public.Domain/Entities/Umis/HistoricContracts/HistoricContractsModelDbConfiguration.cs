using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.HistoricContracts
{
    public class HistoricContractsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new HistoricContractActivityMap());
            modelBuilder.Configurations.Add(new HistoricContractActuallyPaidAmountMap());
            modelBuilder.Configurations.Add(new HistoricContractContractedAmountMap());
            modelBuilder.Configurations.Add(new HistoricContractLocationMap());
            modelBuilder.Configurations.Add(new HistoricContractPartnerMap());
            modelBuilder.Configurations.Add(new HistoricContractProcurementPlanMap());
            modelBuilder.Configurations.Add(new HistoricContractProcurementPlanPositionMap());
            modelBuilder.Configurations.Add(new HistoricContractReimbursedAmountMap());
            modelBuilder.Configurations.Add(new HistoricContractMap());
        }
    }
}
