using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class BudgetLevelsReportItem
    {
        public BudgetLevel BudgetLevel { get; set; }

        public string BudgetLevel1Name { get; set; }

        public string BudgetLevel2Name { get; set; }

        public string ProcedureName { get; set; }

        public decimal? ContractedEuAmount { get; set; }

        public decimal? ContractedBgAmount { get; set; }

        public decimal? ContractedBfpTotalAmount { get; set; }

        public decimal? ContractedSelfAmount { get; set; }

        public decimal? ContractedTotalAmount { get; set; }

        public decimal? ReportedEuAmount { get; set; }

        public decimal? ReportedBgAmount { get; set; }

        public decimal? ReportedBfpTotalAmount { get; set; }

        public decimal? ReportedSelfAmount { get; set; }

        public decimal? ReportedTotalAmount { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? CertifiedEuAmount { get; set; }

        public decimal? CertifiedBgAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }
    }
}
