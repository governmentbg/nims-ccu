using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractBudgetLevel3NomsRepository : IEntityNomsRepository<ContractBudgetLevel3Amount, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetContractContractBudgetLevel3s(int? contractContractId = null, string term = null, int offset = 0, int? limit = null);
    }
}