using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportSAPDataVO
    {
        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public string ProgrammeCode { get; set; }

        public int ProgrammePriorityId { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProcedureCode { get; set; }

        public string ContractCode { get; set; }

        public int? PaymentOrderNum { get; set; }

        public string CompanyUin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType CompanyUinType { get; set; }

        public string CompanyName { get; set; }

        public decimal? PaidEuAmount { get; set; }

        public decimal? PaidBgAmount { get; set; }

        public decimal? PaidCrossAmount { get; set; }

        public decimal? PaidBfpTotalAmount { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedCrossAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Currency Currency { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? CheckedDate { get; set; }

        public DateTime CurrentDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportType ReportType { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportPaymentType PaymentType { get; set; }
    }
}
