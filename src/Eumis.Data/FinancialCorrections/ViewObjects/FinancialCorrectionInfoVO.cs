using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;

namespace Eumis.Data.FinancialCorrections.ViewObjects
{
    public class FinancialCorrectionInfoVO
    {
        public int FinancialCorrectionId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public FinancialCorrectionStatus Status { get; set; }
    }
}
