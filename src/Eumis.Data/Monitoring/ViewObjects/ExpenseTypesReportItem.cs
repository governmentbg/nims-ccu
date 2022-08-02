using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class ExpenseTypesReportItem
    {
        public string Programme { get; set; }

        public string ExpenseType { get; set; }

        public decimal? ApprovedBfpAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? ApprovedTotalAmount { get; set; }

        public decimal? CertifiedBfpAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public decimal? CertifiedTotalAmount { get; set; }
    }
}
