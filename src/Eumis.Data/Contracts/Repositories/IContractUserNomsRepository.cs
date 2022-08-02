using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Users;
using System.Collections.Generic;

namespace Eumis.Data.Contracts.Repositories
{
    public interface IContractUserNomsRepository : IEntityNomsRepository<User, EntityNomVO>
    {
        IList<EntityNomVO> GetContractUserNoms(int contractId, string term, int offset = 0, int? limit = null);

        IList<EntityNomVO> GetContractUser(int userId, string term, int offset = 0, int? limit = null);
    }
}
