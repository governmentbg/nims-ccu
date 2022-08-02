using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using System.Collections.Generic;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IEvalSessionSheetStatusEnumNomsRepository : IEnumNomsRepository<EvalSessionSheetStatus>
    {
        IList<EnumNomVO<EvalSessionSheetStatus>> GetNoms(EvalSessionSheetStatus[] ids, string term);
    }
}
