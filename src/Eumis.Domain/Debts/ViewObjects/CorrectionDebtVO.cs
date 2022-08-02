using System;

namespace Eumis.Domain.Debts.ViewObjects
{
    public class CorrectionDebtVO
    {
        public int CorrectionDebtId { get; set; }

        public int OrderNum { get; set; }

        public string CorrectionRegNumber { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public decimal? DebtEuAmount { get; set; }

        public decimal? DebtBgAmount { get; set; }

        public decimal? DebtTotalAmount { get; set; }

        public decimal? CertEuAmount { get; set; }

        public decimal? CertBgAmount { get; set; }

        public decimal? CertTotalAmount { get; set; }

        public decimal? ReimbursedEuAmount { get; set; }

        public decimal? ReimbursedBgAmount { get; set; }

        public decimal? ReimbursedTotalAmount { get; set; }
    }
}
