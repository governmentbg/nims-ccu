using Eumis.Common.Db;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.FlatFinancialCorrectionLevelItems;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using System.Data.Entity;

namespace Eumis.Domain.MonitoringFinancialControl
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
            modelBuilder.Configurations.Add(new ActuallyPaidAmountDocumentMap());
            modelBuilder.Configurations.Add(new ReimbursedAmountMap());
            modelBuilder.Configurations.Add(new DebtReimbursedAmountMap());
            modelBuilder.Configurations.Add(new ContractReimbursedAmountMap());
            modelBuilder.Configurations.Add(new ContractReimbursedAmountPaymentMap());
            modelBuilder.Configurations.Add(new CompensationDocumentMap());
            modelBuilder.Configurations.Add(new CompensationDocumentDocMap());
            modelBuilder.Configurations.Add(new PrognosisMap());
        }
    }
}
