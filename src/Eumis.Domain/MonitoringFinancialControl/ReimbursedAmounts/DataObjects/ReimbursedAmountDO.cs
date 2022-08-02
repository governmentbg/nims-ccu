using System;
using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System.Linq;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DataObjects
{
    public class ReimbursedAmountDO
    {
        public ReimbursedAmountDO()
        {
        }

        public ReimbursedAmountDO(ContractReimbursedAmount reimbursedAmount)
        {
            this.ReimbursedAmountId = reimbursedAmount.ReimbursedAmountId;
            this.ContractId = reimbursedAmount.ContractId;
            this.ProgrammePriorityId = reimbursedAmount.ProgrammePriorityId;
            this.ReimbursementDate = reimbursedAmount.ReimbursementDate;
            this.Type = reimbursedAmount.Type;
            this.Reimbursement = reimbursedAmount.Reimbursement;

            this.Discriminator = reimbursedAmount.Discriminator;

            this.PrincipalBfpEuAmount = reimbursedAmount.PrincipalBfp.EuAmount;
            this.PrincipalBfpBgAmount = reimbursedAmount.PrincipalBfp.BgAmount;
            this.PrincipalBfpTotalAmount = reimbursedAmount.PrincipalBfp.TotalAmount;

            this.InterestBfpEuAmount = reimbursedAmount.InterestBfp.EuAmount;
            this.InterestBfpBgAmount = reimbursedAmount.InterestBfp.BgAmount;
            this.InterestBfpTotalAmount = reimbursedAmount.InterestBfp.TotalAmount;

            this.Comment = reimbursedAmount.Comment;

            this.PaymentIds = reimbursedAmount.ContractReimbursedAmountPayments.Select(t => t.ContractReportPaymentId).ToArray();

            this.Version = reimbursedAmount.Version;
        }

        public ReimbursedAmountDO(DebtReimbursedAmount reimbursedAmount, int programmePriorityId)
        {
            this.ReimbursedAmountId = reimbursedAmount.ReimbursedAmountId;
            this.ContractId = reimbursedAmount.ContractId;
            this.ProgrammePriorityId = programmePriorityId;
            this.ReimbursementDate = reimbursedAmount.ReimbursementDate;
            this.Type = reimbursedAmount.Type;
            this.Reimbursement = reimbursedAmount.Reimbursement;

            this.Discriminator = reimbursedAmount.Discriminator;

            this.PrincipalBfpEuAmount = reimbursedAmount.PrincipalBfp.EuAmount;
            this.PrincipalBfpBgAmount = reimbursedAmount.PrincipalBfp.BgAmount;
            this.PrincipalBfpTotalAmount = reimbursedAmount.PrincipalBfp.TotalAmount;

            this.InterestBfpEuAmount = reimbursedAmount.InterestBfp.EuAmount;
            this.InterestBfpBgAmount = reimbursedAmount.InterestBfp.BgAmount;
            this.InterestBfpTotalAmount = reimbursedAmount.InterestBfp.TotalAmount;

            this.Comment = reimbursedAmount.Comment;

            this.Version = reimbursedAmount.Version;
        }

        public int ReimbursedAmountId { get; set; }

        public int ContractId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public DateTime? ReimbursementDate { get; set; }

        public ReimbursementType? Type { get; set; }

        public Reimbursement? Reimbursement { get; set; }

        public ReimbursedAmountDiscriminator Discriminator { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PrincipalBfpEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PrincipalBfpBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? PrincipalBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? InterestBfpEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? InterestBfpBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? InterestBfpTotalAmount { get; set; }

        public string Comment { get; set; }

        public int[] PaymentIds { get; set; }

        public byte[] Version { get; set; }
    }
}
