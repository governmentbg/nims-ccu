using System;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;

namespace Eumis.Data.FinancialCorrections.ViewObjects
{
    public class FinancialCorrectionVO
    {
        public int FinancialCorrectionId { get; set; }

        public int OrderNum { get; set; }

        public DateTime ImpositionDate { get; set; }

        public FinancialCorrectionStatus Status { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNumber { get; set; }

        public string ContractContractNumber { get; set; }

        public string ContractCompany { get; set; }

        public string ContractContractorCompany { get; set; }

        public string ContractBudgetLevel3Name { get; set; }

        public string FinancialCorrectionVersionOrderNum { get; set; }

        public decimal? FinancialCorrectionVersionPercent { get; set; }

        public decimal? FinancialCorrectionVersionTotalAmount { get; set; }

        public string ImposingReason { get; set; }
    }
}
