using System;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractItemVO
    {
        public int ItemId { get; set; }

        public string ProcedureName { get; set; }

        public string Name { get; set; }

        public string RegNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractExecutionStatus? ExecutionStatus { get; set; }

        public string Company { get; set; }

        public string CompanyKidCode { get; set; }
    }
}
