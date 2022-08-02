using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractNomsRepository : IEntityNomsRepository<Contract, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetContracts(string term, int offset = 0, int? limit = null, int[] programmeIds = null, int? userId = null);

        IEnumerable<EntityNomVO> GetContracts(int procedureId, string term, int offset = 0, int? limit = null);
    }
}