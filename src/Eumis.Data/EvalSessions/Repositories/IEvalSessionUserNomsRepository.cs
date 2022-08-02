using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Users;
using System.Collections.Generic;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IEvalSessionUserNomsRepository : IEntityNomsRepository<User, EntityNomVO>
    {
        IList<EntityNomVO> GetSessionUserNoms(int evalSessionId, string term, int offset = 0, int? limit = null);
    }
}
