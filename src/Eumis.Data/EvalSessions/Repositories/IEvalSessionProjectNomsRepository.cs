using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using System.Collections.Generic;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IEvalSessionProjectNomsRepository : IRepository
    {
        EntityNomVO GetNom(int nomValueId, int evalSessionId);

        IEnumerable<EntityNomVO> GetNoms(int evalSessionId, string term, int offset = 0, int? limit = null, bool? notDeletedOnly = true);
    }
}
