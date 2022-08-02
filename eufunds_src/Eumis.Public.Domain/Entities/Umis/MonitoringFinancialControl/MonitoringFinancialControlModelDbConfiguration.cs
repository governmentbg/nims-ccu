using System.Data.Entity;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Public.Common.Db;

namespace Eumis.Public.Domain.Entities.Umis.MonitoringFinancialControl
{
    public class MonitoringFinancialControlModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FlatFinancialCorrectionMap());
            modelBuilder.Configurations.Add(new FlatFinancialCorrectionLevelItemMap());
            modelBuilder.Configurations.Add(new FlatFinancialCorrectionProgrammeItemMap());
            modelBuilder.Configurations.Add(new FlatFinancialCorrectionProgrammePriorityItemMap());
            modelBuilder.Configurations.Add(new FlatFinancialCorrectionProcedureItemMap());
            modelBuilder.Configurations.Add(new FlatFinancialCorrectionContractItemMap());
            modelBuilder.Configurations.Add(new FlatFinancialCorrectionContractContractItemMap());
            modelBuilder.Configurations.Add(new OtherViolationMap());
            modelBuilder.Configurations.Add(new FinancialCorrectionImposingReasonMap());
            modelBuilder.Configurations.Add(new FinancialCorrectionMap());
            modelBuilder.Configurations.Add(new FinancialCorrectionVersionMap());
            modelBuilder.Configurations.Add(new FinancialCorrectionVersionViolationMap());
            modelBuilder.Configurations.Add(new ActuallyPaidAmountMap());
            modelBuilder.Configurations.Add(new ReimbursedAmountMap());
            modelBuilder.Configurations.Add(new DebtReimbursedAmountMap());
            modelBuilder.Configurations.Add(new ContractReimbursedAmountMap());
            modelBuilder.Configurations.Add(new CompensationDocumentMap());
            modelBuilder.Configurations.Add(new CompensationDocumentDocMap());
            modelBuilder.Configurations.Add(new PrognosisMap());
        }
    }
}
