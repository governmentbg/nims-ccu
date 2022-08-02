using System;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DataObjects
{
    public class NewDebtReimbursedAmountDO
    {
        public int? ContractDebtId { get; set; }

        public DateTime? ReimbursementDate { get; set; }

        public ReimbursementType? Type { get; set; }

        public Reimbursement? Reimbursement { get; set; }
    }
}
