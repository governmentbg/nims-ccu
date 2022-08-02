using Eumis.Domain.Debts;
using Eumis.Domain.Debts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.Debts.Repositories
{
    public interface IContractDebtVersionsRepository : IAggregateRepository<ContractDebtVersion>
    {
        IList<ContractDebtVersionVO> GetContractDebtVersions(int contractDebtId);

        bool HasContractDebtVersionsInProgress(int contractDebtId);

        bool HasNonDraftContractDebtVersions(int contractDebtId);

        ContractDebtVersion GetActualVersion(int contractDebtId);

        void RemoveByContractDebtId(int contractDebtId);
    }
}
