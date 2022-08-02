using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractContractNomsRepository : IEntityNomsRepository<ContractContract, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetContractContracts(int? contractId = null, string term = null, int offset = 0, int? limit = null);
    }
}