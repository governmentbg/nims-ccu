using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.FIReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Web.Api.ReimbursedAmounts.DataObjects
{
    public class NewFIReimbursedAmountDO
    {
        public NewFIReimbursedAmountDO()
        {
        }

        public NewFIReimbursedAmountDO(Contract contract)
        {
            this.Contract = new ContractDataDO(contract);
        }

        public DateTime? ReimbursementDate { get; set; }

        public FIReimbursementType? Type { get; set; }

        public Reimbursement? Reimbursement { get; set; }

        public ContractDataDO Contract { get; set; }

        public int? ProgrammePriorityId { get; set; }
    }
}
