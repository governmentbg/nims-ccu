using Eumis.Domain.Contracts;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class ContractsReportContractAmountsItem
    {
        public int ContractId { get; set; }

        public ContractExecutionStatus? ContractExecutionStatus { get; set; }

        public string ContractBudgetLevel3AmountNutsFullPath { get; set; }

        public string ContractBudgetLevel3AmountNutsFullPathName { get; set; }

        public decimal? InitialContractedTotalAmount { get; set; }

        public decimal? InitialContractedBfpTotalAmount { get; set; }

        public decimal? InitialContractedEuAmount { get; set; }

        public decimal? InitialContractedBgAmount { get; set; }

        public decimal? InitialContractedSelfAmount { get; set; }

        public decimal? ActualContractedTotalAmount { get; set; }

        public decimal? ActualContractedBfpTotalAmount { get; set; }

        public decimal? ActualContractedEuAmount { get; set; }

        public decimal? ActualContractedBgAmount { get; set; }

        public decimal? ActualContractedSelfAmount { get; set; }

        public decimal? ReportedTotalAmount { get; set; }

        public decimal? ReportedBfpTotalAmount { get; set; }

        public decimal? ReportedEuAmount { get; set; }

        public decimal? ReportedBgAmount { get; set; }

        public decimal? ReportedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? UnapprovedTotalAmount { get; set; }

        public decimal? UnapprovedBfpTotalAmount { get; set; }

        public decimal? UnapprovedEuAmount { get; set; }

        public decimal? UnapprovedBgAmount { get; set; }

        public decimal? UnapprovedSelfAmount { get; set; }

        public decimal? CorrectedTotalAmount { get; set; }

        public decimal? CorrectedBfpTotalAmount { get; set; }

        public decimal? CorrectedEuAmount { get; set; }

        public decimal? CorrectedBgAmount { get; set; }

        public decimal? CorrectedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedEuAmount { get; set; }

        public decimal? CertifiedBgAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }
    }
}
