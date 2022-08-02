using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportAdvancePaymentAmountDO
    {
        public ContractReportAdvancePaymentAmountDO()
        {
        }

        public ContractReportAdvancePaymentAmountDO(
            ContractReportAdvancePaymentAmount contractReportAdvancePaymentAmount,
            ContractReportPayment contractReportPayment,
            string checkedByUser,
            string certCheckedByUser = null)
        {
            this.ContractReportAdvancePaymentAmountId = contractReportAdvancePaymentAmount.ContractReportAdvancePaymentAmountId;
            this.ContractReportPaymentId = contractReportAdvancePaymentAmount.ContractReportPaymentId;
            this.ContractReportId = contractReportAdvancePaymentAmount.ContractReportId;
            this.ContractId = contractReportAdvancePaymentAmount.ContractId;
            this.Gid = contractReportAdvancePaymentAmount.Gid;

            this.Status = contractReportAdvancePaymentAmount.Status;
            this.Approval = contractReportAdvancePaymentAmount.Approval;
            this.Notes = contractReportAdvancePaymentAmount.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportAdvancePaymentAmount.CheckedDate;

            this.ProgrammePriorityId = contractReportAdvancePaymentAmount.ProgrammePriorityId;
            this.ApprovedEuAmount = contractReportAdvancePaymentAmount.ApprovedEuAmount;
            this.ApprovedBgAmount = contractReportAdvancePaymentAmount.ApprovedBgAmount;
            this.ApprovedBfpTotalAmount = contractReportAdvancePaymentAmount.ApprovedBfpTotalAmount;

            this.CertStatus = contractReportAdvancePaymentAmount.CertStatus;
            this.CertCheckedByUser = certCheckedByUser;
            this.CertCheckedDate = contractReportAdvancePaymentAmount.CertCheckedDate;

            this.UncertifiedApprovedEuAmount = contractReportAdvancePaymentAmount.UncertifiedApprovedEuAmount;
            this.UncertifiedApprovedBgAmount = contractReportAdvancePaymentAmount.UncertifiedApprovedBgAmount;
            this.UncertifiedApprovedBfpTotalAmount = contractReportAdvancePaymentAmount.UncertifiedApprovedBfpTotalAmount;

            this.CertifiedApprovedEuAmount = contractReportAdvancePaymentAmount.CertifiedApprovedEuAmount;
            this.CertifiedApprovedBgAmount = contractReportAdvancePaymentAmount.CertifiedApprovedBgAmount;
            this.CertifiedApprovedBfpTotalAmount = contractReportAdvancePaymentAmount.CertifiedApprovedBfpTotalAmount;

            this.ModifyDate = contractReportAdvancePaymentAmount.ModifyDate;
            this.CreateDate = contractReportAdvancePaymentAmount.CreateDate;
            this.Version = contractReportAdvancePaymentAmount.Version;

            this.ContractReportPayment = new ContractReportPaymentDO(contractReportPayment);
        }

        public int ContractReportAdvancePaymentAmountId { get; set; }

        public int ContractReportPaymentId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public ContractReportAdvancePaymentAmountStatus Status { get; set; }

        public ContractReportAdvancePaymentAmountApproval? Approval { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public int ProgrammePriorityId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ApprovedBfpTotalAmount { get; set; }

        public ContractReportAdvancePaymentAmountCertStatus? CertStatus { get; set; }

        public string CertCheckedByUser { get; set; }

        public DateTime? CertCheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? UncertifiedApprovedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedApprovedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedApprovedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedApprovedBfpTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractReportPaymentDO ContractReportPayment { get; set; }
    }
}