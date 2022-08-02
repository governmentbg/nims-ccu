using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using System.Collections.Generic;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IProcedureApplicationDocNomsRepository : IEntityNomsRepository<ProcedureApplicationDoc, EntityNomVO>
    {
        IList<EntityNomVO> GetProcedureApplicationDocNoms(
            int procedureId,
            string term,
            int offset = 0,
            int? limit = null);
    }
}
