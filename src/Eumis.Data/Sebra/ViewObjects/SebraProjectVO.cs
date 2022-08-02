using System;

namespace Eumis.Data.Sebra.ViewObjects
{
    public class SebraProjectVO
    {
        public int ProjectId { get; set; }

        public DateTime RegDate { get; set; }

        public string RegNumber { get; set; }

        public string BeneficiaryName { get; set; }

        public string BankAccount { get; set; }

        public decimal? PaidBfpTotalAmount { get; set; }
    }
}
