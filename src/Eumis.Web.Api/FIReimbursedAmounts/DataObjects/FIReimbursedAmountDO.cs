using System;
using Eumis.Common.Json;
using Eumis.Domain.FIReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Web.Api.ReimbursedAmounts.DataObjects
{
    public class FIReimbursedAmountDO
    {
        public FIReimbursedAmountDO()
        {
        }

        public FIReimbursedAmountDO(FIReimbursedAmount fiReimbursedAmount)
        {
            this.ReimbursedAmountId = fiReimbursedAmount.FIReimbursedAmountId;
            this.ContractId = fiReimbursedAmount.ContractId;
            this.ProgrammePriorityId = fiReimbursedAmount.ProgrammePriorityId;
            this.ReimbursementDate = fiReimbursedAmount.ReimbursementDate;
            this.Type = fiReimbursedAmount.Type;
            this.Reimbursement = fiReimbursedAmount.Reimbursement;

            this.PrincipalBfpEuAmount = fiReimbursedAmount.PrincipalBfp.EuAmount;
            this.PrincipalBfpBgAmount = fiReimbursedAmount.PrincipalBfp.BgAmount;
            this.PrincipalBfpTotalAmount = fiReimbursedAmount.PrincipalBfp.TotalAmount;

            this.InterestBfpEuAmount = fiReimbursedAmount.InterestBfp.EuAmount;
            this.InterestBfpBgAmount = fiReimbursedAmount.InterestBfp.BgAmount;
            this.InterestBfpTotalAmount = fiReimbursedAmount.InterestBfp.TotalAmount;

            this.Comment = fiReimbursedAmount.Comment;

            this.Version = fiReimbursedAmount.Version;
        }

        public int ReimbursedAmountId { get; set; }

        public int ContractId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public DateTime? ReimbursementDate { get; set; }

        public FIReimbursementType? Type { get; set; }

        public Reimbursement? Reimbursement { get; set; }

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

        public byte[] Version { get; set; }
    }
}
