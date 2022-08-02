using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportAdvanceNVPaymentAmountDO
    {
        public ContractReportAdvanceNVPaymentAmountDO()
        {
        }

        public ContractReportAdvanceNVPaymentAmountDO(
            ContractReportAdvanceNVPaymentAmount contractReportAdvanceNVPaymentAmount,
            ContractReportPayment contractReportPayment,
            string checkedByUser)
        {
            this.ContractReportAdvanceNVPaymentAmountId = contractReportAdvanceNVPaymentAmount.ContractReportAdvanceNVPaymentAmountId;
            this.ContractReportPaymentId = contractReportAdvanceNVPaymentAmount.ContractReportPaymentId;
            this.ContractReportId = contractReportAdvanceNVPaymentAmount.ContractReportId;
            this.ContractId = contractReportAdvanceNVPaymentAmount.ContractId;
            this.Gid = contractReportAdvanceNVPaymentAmount.Gid;

            this.Status = contractReportAdvanceNVPaymentAmount.Status;
            this.Approval = contractReportAdvanceNVPaymentAmount.Approval;
            this.Notes = contractReportAdvanceNVPaymentAmount.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportAdvanceNVPaymentAmount.CheckedDate;

            this.ProgrammePriorityId = contractReportAdvanceNVPaymentAmount.ProgrammePriorityId;
            this.EuAmount = contractReportAdvanceNVPaymentAmount.EuAmount;
            this.BgAmount = contractReportAdvanceNVPaymentAmount.BgAmount;
            this.BfpTotalAmount = contractReportAdvanceNVPaymentAmount.BfpTotalAmount;

            this.ModifyDate = contractReportAdvanceNVPaymentAmount.ModifyDate;
            this.CreateDate = contractReportAdvanceNVPaymentAmount.CreateDate;
            this.Version = contractReportAdvanceNVPaymentAmount.Version;

            this.ContractReportPayment = new ContractReportPaymentDO(contractReportPayment);
        }

        public int ContractReportAdvanceNVPaymentAmountId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public ContractReportAdvanceNVPaymentAmountStatus Status { get; set; }

        public ContractReportAdvanceNVPaymentAmountApproval? Approval { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public int ProgrammePriorityId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? EuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BfpTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractReportPaymentDO ContractReportPayment { get; set; }
    }
}