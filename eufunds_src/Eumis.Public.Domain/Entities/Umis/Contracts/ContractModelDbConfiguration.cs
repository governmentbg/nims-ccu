using System.Data.Entity;
using Eumis.Public.Domain.Entities.Umis.Contracts.ContractReportMicros;
using Eumis.Public.Domain.Entities.Umis.Contracts.Views;
using Eumis.Public.Common.Db;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public class ContractModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContractRegistrationMap());
            modelBuilder.Configurations.Add(new ContractMap());
            modelBuilder.Configurations.Add(new ContractLocationMap());
            modelBuilder.Configurations.Add(new ContractBudgetLevel3AmountMap());
            modelBuilder.Configurations.Add(new ContractActivityMap());
            modelBuilder.Configurations.Add(new ContractActivityCompanyMap());
            modelBuilder.Configurations.Add(new ContractPartnerMap());
            modelBuilder.Configurations.Add(new ContractContractorMap());
            modelBuilder.Configurations.Add(new ContractContractMap());
            modelBuilder.Configurations.Add(new ContractSubcontractMap());
            modelBuilder.Configurations.Add(new ContractContractActivityMap());
            modelBuilder.Configurations.Add(new ContractProcurementPlanMap());
            modelBuilder.Configurations.Add(new ContractProcurementPlanPublicDocumentMap());
            modelBuilder.Configurations.Add(new ContractDifferentiatedPositionMap());
            modelBuilder.Configurations.Add(new ContractIndicatorMap());
            modelBuilder.Configurations.Add(new ContractVersionXmlMap());
            modelBuilder.Configurations.Add(new ContractVersionXmlAmountMap());
            modelBuilder.Configurations.Add(new ContractProcurementXmlMap());
            modelBuilder.Configurations.Add(new ContractSpendingPlanXmlMap());
            modelBuilder.Configurations.Add(new ContractsContractRegistrationMap());
            modelBuilder.Configurations.Add(new ContractCommunicationXmlMap());
            modelBuilder.Configurations.Add(new ContractAccessCodeMap());
            modelBuilder.Configurations.Add(new VwAccessCodeMap());
            modelBuilder.Configurations.Add(new ContractReportMap());
            modelBuilder.Configurations.Add(new ContractReportPaymentMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialMap());
            modelBuilder.Configurations.Add(new ContractReportTechnicalMap());
            modelBuilder.Configurations.Add(new ContractReportTechnicalCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportTechnicalCorrectionIndicatorMap());            
            modelBuilder.Configurations.Add(new ContractReportFinancialCheckMap());
            modelBuilder.Configurations.Add(new ContractReportPaymentCheckMap());
            modelBuilder.Configurations.Add(new ContractReportTechnicalCheckMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialCSDMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialCSDBudgetItemMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialCSDFileMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialCorrectionCSDMap());
            modelBuilder.Configurations.Add(new ContractReportAttachedFinancialCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportIndicatorMap());
            modelBuilder.Configurations.Add(new ContractReportAdvancePaymentAmountMap());
            modelBuilder.Configurations.Add(new ContractReportCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportCorrectionDocumentMap());
            modelBuilder.Configurations.Add(new ContractReportRevalidationMap());
            modelBuilder.Configurations.Add(new ContractReportRevalidationDocumentMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialRevalidationMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialRevalidationCSDMap());
            modelBuilder.Configurations.Add(new ContractReportCertCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportCertCorrectionDocumentMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialCertCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialCertCorrectionCSDMap());
            modelBuilder.Configurations.Add(new ContractReportMicroMap());
            modelBuilder.Configurations.Add(new ContractReportMicrosType1ItemMap());
            modelBuilder.Configurations.Add(new ContractReportMicrosType2ItemMap());
            modelBuilder.Configurations.Add(new ContractReportMicrosType3ItemMap());
            modelBuilder.Configurations.Add(new ContractReportMicrosType3ItemFoodDataMap());
            modelBuilder.Configurations.Add(new ContractReportMicrosType4ItemMap());
            modelBuilder.Configurations.Add(new ContractReportMicrosDistrictMap());
            modelBuilder.Configurations.Add(new ContractReportMicrosMunicipalityMap());
            modelBuilder.Configurations.Add(new ContractReportMicrosSettlementMap());
            modelBuilder.Configurations.Add(new ContractReportMicroCheckMap());
            modelBuilder.Configurations.Add(new ContractReportAdvanceNVPaymentAmountMap());
            modelBuilder.Configurations.Add(new ContractReportPaymentCheckAmountMap());
            modelBuilder.Configurations.Add(new ContractExtensionMap());
            modelBuilder.Configurations.Add(new ContractExtensionVesselIdentifierMap());
        }
    }
}
