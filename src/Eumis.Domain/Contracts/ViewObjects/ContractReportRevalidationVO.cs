using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportRevalidationVO
    {
        public int ContractReportRevalidationId { get; set; }

        public string ProgrammeName { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationStatus StatusDescr { get; set; }

        public ContractReportRevalidationStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationType Type { get; set; }

        public DateTime Date { get; set; }

        public decimal? BfpTotalAmount { get; set; }

        public decimal? SelfAmount { get; set; }
    }
}
