using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Newtonsoft.Json;

namespace Eumis.Data.FinancialCorrections.ViewObjects
{
    public class FinancialCorrectionVersionVO
    {
        public int FinancialCorrectionVersionId { get; set; }

        public int FinancialCorrectionId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FinancialCorrectionVersionStatus Status { get; set; }

        public decimal? Percent { get; set; }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public AmendmentReason? AmendmentReason { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CorrectionBearer? CorrectionBearer { get; set; }
    }
}