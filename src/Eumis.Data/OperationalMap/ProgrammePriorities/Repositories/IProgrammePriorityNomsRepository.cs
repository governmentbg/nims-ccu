using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.ProgrammePriorities;

namespace Eumis.Data.OperationalMap.ProgrammePriorities.Repositories
{
    public interface IProgrammePriorityNomsRepository : IEntityNomsRepository<ProgrammePriority, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetProgrammePriorityNoms(string term, int offset = 0, int? limit = null, int? programmeId = null);

        IEnumerable<EntityNomVO> GetProcedureProgrammePriorityNoms(int procedureId, int programmeId, string term, int offset = 0, int? limit = null);

        IEnumerable<EntityNomVO> GetContractProgrammePriorityNoms(int contractId, string term, int offset = 0, int? limit = null);
    }
}
