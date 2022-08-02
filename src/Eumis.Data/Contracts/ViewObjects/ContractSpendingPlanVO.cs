using System;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractSpendingPlanVO
    {
        public int ContractSpendingPlanId { get; set; }

        public int ContactId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Source Source { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractSpendingPlanStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
