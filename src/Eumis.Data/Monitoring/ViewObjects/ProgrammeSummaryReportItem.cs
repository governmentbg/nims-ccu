using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class ProgrammeSummaryReportItem
    {
        public string ProgrammeName { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProcedureName { get; set; }

        public string ContractRegNum { get; set; }

        public decimal? ProgrammeBudgetEuAmount { get; set; }

        public decimal? ProgrammeBudgetBgAmount { get; set; }

        public decimal? ProgrammeBudgetBfpTotalAmount { get; set; }

        public decimal? ContractedEuAmount { get; set; }

        public decimal? ContractedBgAmount { get; set; }

        public decimal? ContractedSelfAmount { get; set; }

        public decimal? ContractedTotalAmount { get; set; }

        public decimal? ReportedEuAmount { get; set; }

        public decimal? ReportedBgAmount { get; set; }

        public decimal? ReportedSelfAmount { get; set; }

        public decimal? ReportedTotalAmount { get; set; }

        public decimal? ActuallyPaidEuAmount { get; set; }

        public decimal? ActuallyPaidBgAmount { get; set; }

        public decimal? ActuallyPaidSelfAmount { get; set; }

        public decimal? ActuallyPaidTotalAmount { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? CertifiedEuAmount { get; set; }

        public decimal? CertifiedBgAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }

        public bool AreAmountsNull { get; set; }
    }
}
