using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.Programmes;

namespace Eumis.Data.OperationalMap.Programmes.Repositories
{
    public interface IProgrammeNomsRepository : IEntityNomsRepository<Programme, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetProgrammeNoms(string term, int offset = 0, int? limit = null, int? procedureId = null, int[] programmeIds = null);
    }
}