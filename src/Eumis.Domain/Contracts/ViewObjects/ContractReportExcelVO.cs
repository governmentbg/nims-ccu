using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportExcelVO
    {
        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportType ReportType { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportStatus Status { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? CheckedDate { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNum { get; set; }

        public string ProcedureName { get; set; }
    }
}
