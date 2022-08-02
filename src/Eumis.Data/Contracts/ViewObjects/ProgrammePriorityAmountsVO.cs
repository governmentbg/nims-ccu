using System.Collections.Generic;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ProgrammePriorityAmountsVO
    {
        public ProgrammePriorityAmountsVO()
        {
            this.EuAmounts = new List<FinanceSourceAmountsVO>();
        }

        public string ProgrammePriorityCode { get; set; }

        public decimal? TotalAmountBGN { get; set; }

        public decimal? TotalAmountEUR { get; set; }

        public decimal? BgAmountBGN { get; set; }

        public decimal? BgAmountEUR { get; set; }

        public decimal? TotalSelfAmountBGN { get; set; }

        public decimal? TotalSelfAmountEUR { get; set; }

        public decimal? BfpTotalAmountBGN { get; set; }

        public decimal? BfpTotalAmountEUR { get; set; }

        public IEnumerable<FinanceSourceAmountsVO> EuAmounts { get; set; }
    }
}
