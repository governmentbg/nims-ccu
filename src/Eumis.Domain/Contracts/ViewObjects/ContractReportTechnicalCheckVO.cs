using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportTechnicalCheckVO
    {
        public int ContractReportTechnicalCheckId { get; set; }

        public int ContractReportTechnicalId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportTechnicalCheckStatus Status { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public int TechnicalVersionNum { get; set; }

        public int TechnicalVersionSubNum { get; set; }
    }
}
