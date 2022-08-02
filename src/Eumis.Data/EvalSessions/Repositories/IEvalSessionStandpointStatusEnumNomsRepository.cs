using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IEvalSessionStandpointStatusEnumNomsRepository : IEnumNomsRepository<EvalSessionStandpointStatus>
    {
        IList<EnumNomVO<EvalSessionStandpointStatus>> GetNoms(EvalSessionStandpointStatus[] ids, string term);
    }
}
