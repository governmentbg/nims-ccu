using Eumis.Common.Json;
using Eumis.Domain.MonitoringFinancialControl;
using Newtonsoft.Json;

namespace Eumis.Data.ActuallyPaidAmounts.ViewObjects
{
    public class ActuallyPaidAmountInfoVO
    {
        public string ContractNum { get; set; }

        public ActuallyPaidAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActuallyPaidAmountStatus StatusDescr { get; set; }
    }
}
