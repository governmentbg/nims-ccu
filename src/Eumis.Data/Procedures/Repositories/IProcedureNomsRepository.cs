using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Procedures.Repositories
{
    public interface IProcedureNomsRepository : IEntityNomsRepository<Procedure, EntityNomVO>
    {
        IList<EntityNomVO> GetProcedureNoms(int programmeId, string term, int offset = 0, int? limit = null);

        IList<EntityNomVO> GetProcedureNoms(int[] programmeIds, string term, int offset = 0, int? limit = null);

        IList<EntityNomVO> GetProcedureNoms(string term, int offset = 0, int? limit = null);

        IList<EntityNomVO> GetProcedureNomsByProgramme(int programmeId, string term, int offset = 0, int? limit = null);

        IList<EntityNomVO> GetProcedureNomsByProgrammePriority(int programmePriorityId, string term, int offset = 0, int? limit = null);

        IList<EntityNomVO> GetProcedureNomsByProgrammeAndProgrammePriority(int programmeId, int programmePriorityId, string term, int offset = 0, int? limit = null);

        IList<EntityNomVO> GetActiveProcedureNoms(int[] programmeIds, string term, int offset = 0, int? limit = null);
    }
}
