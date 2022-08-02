using Eumis.Data.CertReports.ViewObjects;
using Eumis.Domain.CertReports;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.CertReports.ViewObjects.SummaryVOs;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.CertReports.Repositories
{
    public interface ICertReportsRepository : IAggregateRepository<CertReport>
    {
        IList<CertReportVO> GetCertReports(int[] programmeIds);

        IList<CertReportVO> GetCertReportChecksCertReports(int[] programmeIds);

        int GetNextOrderNum(int programmeId);

        int GetProgrammeId(int certReportId);

        int GetOrderNum(int certReportId);

        CertReportInfoVO GetInfo(int certReportId);

        IList<CertReportVO> GetCertReportAttachedCertReports(int certReportId);

        IList<CertReportVO> GetCertReportsForAttachedCertReports(int certReportId);

        IList<ContractDebtVO> GetCertReportContractDebts(int certReportId);

        IList<ContractDebtVO> GetContractDebtsForCertReport(int certReportId);

        IList<DebtReimbursedAmountVO> GetCertReportDebtReimbursedAmounts(int certReportId);

        IList<DebtReimbursedAmountVO> GetDebtReimbursedAmountsForCertReport(int certReportId);

        IList<CertReportDocumentsVO> GetCertReportDocuments(int certReportId);

        IList<CertReportCertificationDocumentsVO> GetCertReportCertificationDocuments(int certReportId);

        IList<CertReportVO> GetFinancialCorrectionCertReports(int financialCorrectionId);

        //// start - amounts in certReport tabs
        IList<CertReportPaymentVO> GetCertReportPayments(int certReportId);

        IList<CertReportAdvancePaymentVO> GetCertReportAdvancePayments(int certReportId);

        IList<CertReportFinancialCorrectionVO> GetCertReportFinancialCorrections(int certReportId);

        IList<CertReportCorrectionVO> GetCertReportCorrections(int certReportId);

        IList<CertReportFinancialRevalidationVO> GetCertReportFinancialRevalidations(int certReportId);

        IList<CertReportRevalidationVO> GetCertReportRevalidations(int certReportId);

        IList<CertReportFinancialCertCorrectionVO> GetCertReportFinancialCertCorrections(int certReportId);

        IList<CertReportCertCorrectionVO> GetCertReportCertCorrections(int certReportId);
        //// end - amounts in certReport tabs

        //// start - amounts in certReport modals to choose from
        IList<CertReportPaymentVO> GetContractReportsForCertReportPayments(int certReportId);

        IList<CertReportAdvancePaymentVO> GetContractReportsForCertReportAdvancePayments(int certReportId);

        IList<CertReportFinancialCorrectionVO> GetContractReportFinancialCorrectionsForCertReportFinancialCorrections(int certReportId);

        IList<CertReportCorrectionVO> GetContractReportCorrectionsForCertReportCorrections(int certReportId);

        IList<CertReportFinancialRevalidationVO> GetContractReportFinancialRevalidationsForCertReportFinancialRevalidations(int certReportId);

        IList<CertReportRevalidationVO> GetContractReportRevalidationsForCertReportRevalidations(int certReportId);

        IList<CertReportFinancialCertCorrectionVO> GetContractReportFinancialCertCorrectionsForCertReportFinancialCertCorrections(int certReportId);

        IList<CertReportCertCorrectionVO> GetContractReportCertCorrectionsForCertReportCertCorrections(int certReportId);
        //// end - amounts in certReport modals to choose from

        //// start - certReport summary queries
        CertReportEligibleProgrammePriorityExpensesResultVO GetCertReportIntermediateFinalEligibleProgrammePriorityExpenses(int certReportId);

        CertReportApprovedAmountsCorrectionsResultVO GetCertReportApprovedAmountsCorrections(int certReportId);

        CertReportStateAidPaidAdvancePaymentsResultVO GetCertReportIntermediateFinalStateAidPaidAdvancePayments(int certReportId);

        CertReportReaffirmedCostsByAdministrativeAuthorityResultVO GetCertReportReaffirmedCostsByAdministrativeAuthority(int certReportId);

        CertReportProgrammePaidContributionInfoForFinancialInstrumentsResultVO GetCertReportProgrammePaidContributionInfoForFinancialInstruments(int certReportId);

        CertReportAnnex4aResultVO GetAnnex4A(int certReportId);
        //// end - certReport summary queries

        IList<SapCertReportVO> GetSapCertReports(int? certReportId);

        IQueryable<CertReportApprovedCertifiedGroupedAmountVO> GetCertReportAmountsGroupedByCertReport();
    }
}
