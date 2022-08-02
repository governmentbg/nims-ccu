using Eumis.Common.Json;
using Eumis.Domain.FIReimbursedAmounts;
using Newtonsoft.Json;

namespace Eumis.Data.FIReimbursedAmounts.ViewObjects
{
    public class FIReimbursedAmountInfoVO
    {
        public string ContractRegNumber { get; set; }

        public FIReimbursedAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FIReimbursedAmountStatus StatusDescr { get; set; }
    }
}
