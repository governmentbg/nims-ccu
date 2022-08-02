using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Debts.DataObjects
{
    public class ContractDebtInterestDO
    {
        public ContractDebtInterestDO()
        {
        }

        public ContractDebtInterestDO(ContractDebtInterest contractDebtInterest, byte[] version, bool isLast)
        {
            this.ContractDebtInterestId = contractDebtInterest.ContractDebtInterestId;
            this.ContractDebtId = contractDebtInterest.ContractDebtId;
            this.InterestSchemeId = contractDebtInterest.InterestSchemeId;
            this.OrderNum = contractDebtInterest.OrderNum;
            this.DateFrom = contractDebtInterest.DateFrom;
            this.DateTo = contractDebtInterest.DateTo;
            this.EuInterestAmount = contractDebtInterest.EuInterestAmount;
            this.BgInterestAmount = contractDebtInterest.BgInterestAmount;
            this.TotalInterestAmount = contractDebtInterest.TotalInterestAmount;
            this.EuAmount = contractDebtInterest.EuAmount;
            this.BgAmount = contractDebtInterest.BgAmount;
            this.TotalAmount = contractDebtInterest.TotalAmount;

            this.Version = version;
            this.IsLast = isLast;
        }

        public int? ContractDebtInterestId { get; set; }

        public int? ContractDebtId { get; set; }

        public int? InterestSchemeId { get; set; }

        public int? OrderNum { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? EuInterestAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BgInterestAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TotalInterestAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? EuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TotalAmount { get; set; }

        public byte[] Version { get; set; }

        public bool IsLast { get; set; }

        public string Error { get; set; }
    }
}
