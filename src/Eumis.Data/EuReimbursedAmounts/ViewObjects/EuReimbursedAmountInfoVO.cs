using Eumis.Common.Json;
using Eumis.Domain.EuReimbursedAmounts;
using Newtonsoft.Json;

namespace Eumis.Data.EuReimbursedAmounts.ViewObjects
{
    public class EuReimbursedAmountInfoVO
    {
        public string ProgrammeCode { get; set; }

        public EuReimbursedAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public EuReimbursedAmountStatus StatusDescr { get; set; }

        public byte[] Version { get; set; }
    }
}
