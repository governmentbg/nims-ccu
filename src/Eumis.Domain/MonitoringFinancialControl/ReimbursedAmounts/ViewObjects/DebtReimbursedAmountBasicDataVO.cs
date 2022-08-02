using System.Collections.Generic;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects
{
    public class DebtReimbursedAmountBasicDataVO
    {
        public int ReimbursedAmountId { get; set; }

        public string RegNumber { get; set; }

        public ReimbursedAmountStatus Status { get; set; }

        public bool IsActivated { get; set; }

        public string IsDeletedNote { get; set; }

        public int ProgrammeId { get; set; }

        public string ContractName { get; set; }

        public string ContractRegNumber { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        public UinType CompanyUinType { get; set; }

        public int ContractDebtId { get; set; }

        public int? CertReportNum { get; set; }

        public DebtReimbursedAmountCreationType CreationType { get; set; }

        public IList<DebtReimbursedAmountBasicDataPaymentVO> Payments { get; set; }

        public byte[] Version { get; set; }
    }
}
