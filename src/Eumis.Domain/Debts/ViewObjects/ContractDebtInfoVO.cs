using Eumis.Domain.Debts;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.Debts.ViewObjects
{
    public class ContractDebtInfoVO
    {
        public int ContractDebtId { get; set; }

        public int ContractId { get; set; }

        public int ProgrammePriorityId { get; set; }

        public ContractDebtStatus Status { get; set; }
    }
}
