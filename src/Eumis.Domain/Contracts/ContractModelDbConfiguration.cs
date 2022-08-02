using System.Data.Entity;
using Eumis.Common.Db;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Contracts.Views;

namespace Eumis.Domain.Contracts
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
            modelBuilder.Configurations.Add(new ContractDifferentiatedPositionMap());
            modelBuilder.Configurations.Add(new ContractIndicatorMap());
            modelBuilder.Configurations.Add(new ContractVersionXmlMap());
            modelBuilder.Configurations.Add(new ContractVersionXmlFileMap());
            modelBuilder.Configurations.Add(new ContractProcurementXmlMap());
            modelBuilder.Configurations.Add(new ContractProcurementXmlFileMap());
            modelBuilder.Configurations.Add(new ContractSpendingPlanXmlMap());
            modelBuilder.Configurations.Add(new ContractsContractRegistrationMap());
            modelBuilder.Configurations.Add(new ContractCommunicationXmlMap());
            modelBuilder.Configurations.Add(new ContractCommunicationXmlFileMap());
            modelBuilder.Configurations.Add(new ContractAccessCodeMap());
            modelBuilder.Configurations.Add(new ContractUserMap());
            modelBuilder.Configurations.Add(new VwAccessCodeMap());
            modelBuilder.Configurations.Add(new ContractReportMap());
            modelBuilder.Configurations.Add(new ContractReportPaymentMap());
            modelBuilder.Configurations.Add(new ContractReportPaymentXmlFileMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialFileMap());
            modelBuilder.Configurations.Add(new ContractReportTechnicalMap());
            modelBuilder.Configurations.Add(new ContractReportTechnicalFileMap());
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
            modelBuilder.Configurations.Add(new ContractReportCertAuthorityCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportCertAuthorityCorrectionDocumentMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialCertCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportFinancialCertCorrectionCSDMap());
            modelBuilder.Configurations.Add(new ContractReportCertAuthorityFinancialCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportCertAuthorityFinancialCorrectionCSDMap());
            modelBuilder.Configurations.Add(new ContractReportRevalidationCertAuthorityCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportRevalidationCertAuthorityCorrectionDocumentMap());
            modelBuilder.Configurations.Add(new ContractReportRevalidationCertAuthorityFinancialCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportRevalidationCertAuthorityFinancialCorrectionCSDMap());
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
            modelBuilder.Configurations.Add(new ContractReportTechnicalMemberMap());
            modelBuilder.Configurations.Add(new ContractVersionXmlAmountMap());
            modelBuilder.Configurations.Add(new ContractGrantDocumentMap());
            modelBuilder.Configurations.Add(new ContractProcurementDocumentMap());
            modelBuilder.Configurations.Add(new ContractReportTechnicalCorrectionMap());
            modelBuilder.Configurations.Add(new ContractReportTechnicalCorrectionIndicatorMap());
        }
    }
}
