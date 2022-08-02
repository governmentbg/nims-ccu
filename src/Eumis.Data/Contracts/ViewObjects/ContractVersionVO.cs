using System;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractVersionVO
    {
        public int ContractVersionId { get; set; }

        public int ContractId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractVersionType VersionType { get; set; }

        public string VersionNumber { get; set; }

        public string RegNumber { get; set; }

        public string CreateNote { get; set; }

        public decimal? TotalBfpAmount { get; set; }

        public DateTime? ContractDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractVersionStatus Status { get; set; }
    }
}
