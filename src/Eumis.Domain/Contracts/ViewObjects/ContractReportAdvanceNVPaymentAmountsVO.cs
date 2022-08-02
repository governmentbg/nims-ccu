using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.ViewObjects
{
    public class ContractReportAdvanceNVPaymentAmountsVO
    {
        public int ContractReportAdvanceNVPaymentAmountId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportAdvanceNVPaymentAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportAdvanceNVPaymentAmountApproval? Approval { get; set; }

        public string Notes { get; set; }

        public int? CheckedByUserId { get; set; }

        public DateTime? CheckedDate { get; set; }

        public string ProgrammePriorityName { get; set; }

        public int ProgrammePriorityId { get; set; }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? BfpTotalAmount { get; set; }
    }
}
