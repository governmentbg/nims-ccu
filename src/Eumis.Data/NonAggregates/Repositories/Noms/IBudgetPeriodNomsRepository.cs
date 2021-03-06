using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IBudgetPeriodNomsRepository : IEntityNomsRepository<BudgetPeriod, EntityNomVO>
    {
        IList<EntityNomVO> GetBudgetPeriodNoms(int programmePriorityId, string term, int offset = 0, int? limit = null);
    }
}
