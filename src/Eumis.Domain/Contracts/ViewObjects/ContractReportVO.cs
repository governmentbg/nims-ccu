using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportVO
    {
        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportType ReportType { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportStatus Status { get; set; }

        public string StatusNote { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Source Source { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? SubmitDeadline { get; set; }

        public DateTime? CheckedDate { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNum { get; set; }

        public string ProcedureName { get; set; }

        public int ProcedureId { get; set; }

        public decimal? RequestedAmount { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public decimal? CertifiedAmount { get; set; }
    }
}
