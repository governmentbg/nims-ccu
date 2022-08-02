using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.NonAggregates;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class RegulationInvestmentPriorityNomsRepository : EntityNomsRepository<RegulationInvestmentPriority, EntityNomVO>, IRegulationInvestmentPriorityNomsRepository
    {
        public RegulationInvestmentPriorityNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.InvestmentPriorityId,
                t => t.Code + " " + t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.InvestmentPriorityId,
                    Name = t.Code + " " + t.Name,
                })
        {
        }

        public IList<EntityNomVO> GetNomsForInterventionCategory(
            int interventionCategoryId,
            string term,
            int offset = 0,
            int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<RegulationInvestmentPriority>()
                .AndStringContains(this.nameSelector, term);

            return (from ip in this.unitOfWork.DbContext.Set<RegulationInvestmentPriority>().Where(predicate)
                    where ip.InterventionCategoryId == interventionCategoryId
                    select ip)
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
