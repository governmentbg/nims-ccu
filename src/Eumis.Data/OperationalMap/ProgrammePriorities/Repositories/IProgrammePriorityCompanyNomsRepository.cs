using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.ProgrammePriorities;

namespace Eumis.Data.OperationalMap.ProgrammePriorities.Repositories
{
    public interface IProgrammePriorityCompanyNomsRepository : IEntityNomsRepository<ProgrammePriorityCompany, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetProgrammePriorityCompanyNoms(ProgrammePriorityType type, string term, bool higherOrderCompany = false, int offset = 0, int? limit = null);
    }
}
