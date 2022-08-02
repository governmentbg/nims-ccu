using System.Collections.Generic;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    public interface IRegulationInvestmentPriorityNomsRepository : IEntityNomsRepository<RegulationInvestmentPriority, EntityNomVO>
    {
        IList<EntityNomVO> GetNomsForInterventionCategory(
            int interventionCategoryId,
            string term,
            int offset = 0,
            int? limit = null);
    }
}
