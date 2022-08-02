using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.DataObjects
{
    public class NewContractReimbursedAmountDO
    {
        public NewContractReimbursedAmountDO()
        {
        }

        public NewContractReimbursedAmountDO(Contract contract)
        {
            this.Contract = new ContractDataDO(contract);
        }

        public DateTime? ReimbursementDate { get; set; }

        public ReimbursementType? Type { get; set; }

        public Reimbursement? Reimbursement { get; set; }

        public ContractDataDO Contract { get; set; }

        public int? ProgrammePriorityId { get; set; }
    }
}
