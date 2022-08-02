using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class Anex3ReimbursedAmountVO
    {
        public DateTime? DebtStartDate { get; set; }

        public decimal? DebtBfpTotalAmount { get; set; }

        public decimal? DebtTotalAmount { get; set; }

        public DateTime? ReimbursementDate { get; set; }

        public decimal? ReimbursementBfpTotalAmount { get; set; }

        public decimal? ReimbursementTotalAmount { get; set; }

        public decimal? WrittenOffBfpTotalAmount { get; set; }

        public decimal? WrittenOffTotalAmount { get; set; }
    }
}
