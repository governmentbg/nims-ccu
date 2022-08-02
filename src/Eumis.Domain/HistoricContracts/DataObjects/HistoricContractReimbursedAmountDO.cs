using System;

namespace Eumis.Domain.HistoricContracts.DataObjects
{
    public class HistoricContractReimbursedAmountDO
    {
        public DateTime? ReimbursementDate { get; set; }

        public decimal? ReimbursedPrincipalEuAmount { get; set; }

        public decimal? ReimbursedPrincipalBgAmount { get; set; }
    }
}
