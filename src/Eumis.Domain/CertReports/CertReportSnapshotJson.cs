using System.Collections.Generic;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.Debts.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;

namespace Eumis.Domain.CertReports
{
    public class CertReportSnapshotJson
    {
        public CertReportSnapshotJson()
        {
        }

        public IList<CertReportPaymentVO> CertReportPayments { get; set; }

        public IList<CertReportAdvancePaymentVO> CertReportAdvancePayments { get; set; }

        public IList<CertReportFinancialCorrectionVO> CertReportFinancialCorrections { get; set; }

        public IList<CertReportCorrectionVO> CertReportCorrections { get; set; }

        public IList<CertReportFinancialRevalidationVO> CertReportFinancialRevalidations { get; set; }

        public IList<CertReportRevalidationVO> CertReportRevalidations { get; set; }

        public IList<CertReportFinancialCertCorrectionVO> CertReportFinancialCertCorrections { get; set; }

        public IList<CertReportCertCorrectionVO> CertReportCertCorrections { get; set; }

        public IList<CertReportDocumentsVO> CertReportDocuments { get; set; }

        public IList<CertReportCertificationDocumentsVO> CertReportCertificationDocuments { get; set; }

        public IList<CertReportVO> AttachedCertReports { get; set; }

        public IList<ContractDebtVO> CertReportContractDebts { get; set; }

        public IList<DebtReimbursedAmountVO> CertReportDebtReimbursedAmounts { get; set; }

        public CertReportEligibleProgrammePriorityExpensesResultVO CertReportIntermediateFinalEligibleProgrammePriorityExpenses { get; set; }

        public CertReportApprovedAmountsCorrectionsResultVO CertReportApprovedAmountsCorrections { get; set; }

        public CertReportStateAidPaidAdvancePaymentsResultVO CertReportIntermediateFinalStateAidPaidAdvancePayments { get; set; }

        public CertReportReaffirmedCostsByAdministrativeAuthorityResultVO CertReportReaffirmedCostsByAdministrativeAuthority { get; set; }

        public CertReportProgrammePaidContributionInfoForFinancialInstrumentsResultVO CertReportProgrammePaidContributionInfoForFinancialInstruments { get; set; }
    }
}
