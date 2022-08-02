using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IProcedureMonitorstatRequestNomsRepository : IEntityNomsRepository<ProcedureMonitorstatRequest, EntityNomVO>
    {
        IList<EntityNomVO> GetNomsForProcedure(
            int projectId,
            string term,
            int offset = 0,
            int? limit = null);

        IList<EntityNomVO> GetNSIDeclarationNomsForProcedure(
            int projectId,
            string term,
            int offset = 0,
            int? limit = null);
    }
}
