using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.DataObjects;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Debts.DataObjects
{
    public class NewContractDebtDO
    {
        public NewContractDebtDO()
        {
            this.PaymentIds = new List<int>().ToArray();
        }

        public NewContractDebtDO(Contract contract)
            : this()
        {
            this.Contract = new ContractDataDO(contract);
        }

        public DateTime? RegDate { get; set; }

        public DateTime? DebtStartDate { get; set; }

        public DateTime? InterestStartDate { get; set; }

        public ContractDebtExecutionStatus? ExecutionStatus { get; set; }

        public ContractDataDO Contract { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public int[] PaymentIds { get; set; }
    }
}
