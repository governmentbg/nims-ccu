using Eumis.Common.Json;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportAdvancePaymentAmountsVO
    {
        public int ContractReportAdvancePaymentAmountId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportAdvancePaymentAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportAdvancePaymentAmountApproval? Approval { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public string ProgrammePriorityName { get; set; }

        public int ProgrammePriorityId { get; set; }

        public decimal? ApprovedEuAmount { get; set; }

        public decimal? ApprovedBgAmount { get; set; }

        public decimal? ApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportAdvancePaymentAmountCertStatus? CertStatus { get; set; }

        public decimal? CertifiedApprovedEuAmount { get; set; }

        public decimal? CertifiedApprovedBgAmount { get; set; }

        public decimal? CertifiedApprovedBfpTotalAmount { get; set; }

        // used for cert report snapshot
        public ContractReportAdvancePaymentAmountDO ContractReportAdvancePaymentAmount { get; set; }
    }
}
