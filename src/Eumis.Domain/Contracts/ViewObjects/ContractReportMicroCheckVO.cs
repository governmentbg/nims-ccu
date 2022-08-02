using Eumis.Common.Json;
using Eumis.Domain.Contracts.ContractReportMicros;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportMicroCheckVO
    {
        public int ContractReportMicroCheckId { get; set; }

        public int ContractReportMicroId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportMicroType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportMicroCheckStatus Status { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public int MicroVersionNum { get; set; }

        public int MicroVersionSubNum { get; set; }
    }
}
