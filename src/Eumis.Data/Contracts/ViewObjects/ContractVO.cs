using System;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Newtonsoft.Json;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractVO
    {
        public int ContractId { get; set; }

        public int ProcedureId { get; set; }

        public int ProgrammeId { get; set; }

        public ContractStatus ContractStatus { get; set; }

        public string ProcedureName { get; set; }

        public string Name { get; set; }

        public string RegNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractExecutionStatus? ExecutionStatus { get; set; }

        public string Company { get; set; }

        public string CompanyKidCode { get; set; }

        public decimal? TotalBfpAmount { get; set; }

        public decimal? TotalSelfAmount { get; set; }
    }
}
