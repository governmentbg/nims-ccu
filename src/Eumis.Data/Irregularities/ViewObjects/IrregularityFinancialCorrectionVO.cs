using System;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;

namespace Eumis.Data.Irregularities.ViewObjects
{
    public class IrregularityFinancialCorrectionVO
    {
        public int? IrregularityItemId { get; set; }

        public int FinancialCorrectionId { get; set; }

        public int OrderNum { get; set; }

        public FinancialCorrectionStatus Status { get; set; }

        public DateTime ImpositionDate { get; set; }

        public string ContractContractNumber { get; set; }

        public string ContractContractorCompany { get; set; }

        public string ContractBudgetLevel3Name { get; set; }
    }
}
