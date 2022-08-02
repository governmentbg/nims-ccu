using System;

namespace Eumis.Domain.HistoricContracts.DataObjects
{
    public class HistoricContractActuallyPaidAmountDO
    {
        public DateTime? PaymentDate { get; set; }

        public decimal? PaidEuAmount { get; set; }

        public decimal? PaidBgAmount { get; set; }
    }
}
