using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using System.Collections.Generic;

namespace Eumis.Data.EvalSessions.Repositories
{
    public interface IProcedureEvalTableTypeEnumNomsRepository : IEnumNomsRepository<ProcedureEvalTableType>
    {
        IList<EnumNomVO<ProcedureEvalTableType>> GetEvalSessionSheetTypes(int evalSessionId, string term);
    }
}
