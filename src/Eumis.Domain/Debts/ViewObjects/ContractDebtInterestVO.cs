using System;

namespace Eumis.Domain.Debts.ViewObjects
{
    public class ContractDebtInterestVO
    {
        public int ContractDebtInterestId { get; set; }

        public int ContractDebtId { get; set; }

        public int OrderNum { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public decimal EuInterestAmount { get; set; }

        public decimal BgInterestAmount { get; set; }

        public decimal TotalInterestAmount { get; set; }

        public decimal? EuAmount { get; set; }

        public decimal? BgAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        public string InterestScheme { get; set; }
    }
}