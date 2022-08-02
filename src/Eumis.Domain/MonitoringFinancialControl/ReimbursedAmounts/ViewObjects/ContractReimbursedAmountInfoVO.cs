using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects
{
    public class ContractReimbursedAmountInfoVO
    {
        public string ContractRegNumber { get; set; }

        public ReimbursedAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ReimbursedAmountStatus StatusDescr { get; set; }
    }
}
