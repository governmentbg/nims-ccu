using System;

namespace Eumis.Domain.HistoricContracts.DataObjects
{
    public class HistoricContractContractedAmountDO
    {
        public DateTime? ContractedDate { get; set; }

        public decimal? ContractedEuAmount { get; set; }

        public decimal? ContractedBgAmount { get; set; }

        public decimal? ContractedSeftAmount { get; set; }
    }
}
