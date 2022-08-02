using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.SpotChecks;

namespace Eumis.Data.SpotChecks.Repositories
{
    public interface ISpotCheckPlanNomsRepository : IEntityNomsRepository<SpotCheckPlan, EntityNomVO>
    {
        IEnumerable<EntityNomVO> GetPlanNoms(string term, int offset = 0, int? limit = null, int[] programmeIds = null);
    }
}