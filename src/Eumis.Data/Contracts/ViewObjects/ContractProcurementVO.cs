using System;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractProcurementVO
    {
        public int ContractProcurementId { get; set; }

        public int ContactId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Source Source { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractProcurementStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}
