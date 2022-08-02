using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportCertifiedAmountVO
    {
        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportType ReportType { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Source Source { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNum { get; set; }

        public string ProcedureName { get; set; }

        public decimal CertifiedApprovedAmount { get; set; }
    }
}
