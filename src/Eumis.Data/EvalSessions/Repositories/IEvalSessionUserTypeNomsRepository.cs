using System.Collections.Generic;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IEvalSessionUserTypeNomsRepository : IRepository
    {
        EntityNomVO GetNom(int nomValueId, int evalSessionId);

        IEnumerable<EntityNomVO> GetNoms(int evalSessionId, EvalSessionUserType userType, string term, int offset = 0, int? limit = null);
    }
}
