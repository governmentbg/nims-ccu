using System.Collections.Generic;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Debts;

namespace Eumis.Data.Debts.Repositories
{
    public interface IContractDebtNomsRepository : IEntityNomsRepository<ContractDebt, ContractDebtNomVO>
    {
        IEnumerable<ContractDebtNomVO> GetDebts(string term, int offset = 0, int? limit = null, int? contractId = null, int[] programmeIds = null);
    }
}