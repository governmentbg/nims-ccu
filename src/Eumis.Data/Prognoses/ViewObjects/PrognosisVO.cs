using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Prognoses.ViewObjects
{
    public class PrognosisVO
    {
        public int PrognosisId { get; set; }

        public string Programme { get; set; }

        public string ProgrammePriority { get; set; }

        public string Procedure { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Year Year { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Month Month { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public PrognosisStatus StatusDescr { get; set; }

        public PrognosisStatus Status { get; set; }

        public decimal? ContractedAmount { get; set; }

        public decimal? PaymentAmount { get; set; }

        public decimal? AdvancePaymentAmount { get; set; }

        public decimal? AdvanceVerPaymentAmount { get; set; }

        public decimal? IntermediatePaymentAmount { get; set; }

        public decimal? FinalPaymentAmount { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public decimal? CertifiedAmount { get; set; }
    }
}
