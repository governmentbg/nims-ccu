using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.Contracts.ViewObjects;
using Newtonsoft.Json;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class CertReportPaymentVO
    {
        // contract report payment
        public int ContractReportPaymentId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportPaymentType? PaymentType { get; set; }

        public decimal? RequestedAmount { get; set; }

        public DateTime? PaymentRegDate { get; set; }

        public int? PaymentVersionNum { get; set; }

        // contract report
        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportType ReportType { get; set; }

        public int OrderNum { get; set; }

        public DateTime? CheckedDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportStatus Status { get; set; }

        public string StatusNote { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Source Source { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? SubmitDeadline { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNum { get; set; }

        public string ProcedureName { get; set; }

        public int ProcedureId { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        public decimal? ApprovedSelfAmount { get; set; }

        public decimal? CertifiedBfpTotalAmount { get; set; }

        public decimal? CertifiedSelfAmount { get; set; }

        public ContractReportPaymentCheckDO ContractReportPaymentCheck { get; set; }

        public IList<ContractReportFinancialCSDBudgetItemsVO> ContractReportFinancialCSDBudgetItems { get; set; }
    }
}
