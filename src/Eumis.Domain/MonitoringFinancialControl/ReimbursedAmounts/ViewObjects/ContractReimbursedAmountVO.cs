using System;
using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects
{
    public class ContractReimbursedAmountVO
    {
        public int AmountId { get; set; }

        public string ProgrammeName { get; set; }

        public string ContractRegNumber { get; set; }

        public string RegNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ReimbursedAmountStatus StatusDescr { get; set; }

        public ReimbursedAmountStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ReimbursementType Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Reimbursement Reimbursement { get; set; }

        public DateTime ReimbursementDate { get; set; }

        public decimal? PrincipalEuAmount { get; set; }

        public decimal? PrincipalBgAmount { get; set; }

        public decimal? PrincipalTotalAmount { get; set; }

        public decimal? InterestEuAmount { get; set; }

        public decimal? InterestBgAmount { get; set; }

        public decimal? InterestTotalAmount { get; set; }
    }
}
