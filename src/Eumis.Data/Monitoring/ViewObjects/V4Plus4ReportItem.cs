using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class V4Plus4ReportItem
    {
        public decimal? ProceduresEuAmount { get; set; }

        public decimal? ProceduresBgAmount { get; set; }

        public decimal? ProceduresBfpTotalAmount { get; set; }

        public decimal? ProjectsBfpTotalAmount { get; set; }

        public decimal? ContractsEuAmount { get; set; }

        public decimal? ContractsBgAmount { get; set; }

        public decimal? ContractsBfpTotalAmount { get; set; }

        public decimal? ActuallyPaidEuAmount { get; set; }

        public decimal? ActuallyPaidBgAmount { get; set; }

        public decimal? ActuallyPaidTotalAmount { get; set; }
    }
}
