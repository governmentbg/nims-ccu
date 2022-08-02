using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class BudgetPeriodNomsRepository : EntityNomsRepository<BudgetPeriod, EntityNomVO>, IBudgetPeriodNomsRepository
    {
        public BudgetPeriodNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.BudgetPeriodId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.BudgetPeriodId,
                    Name = t.Name,
                })
        {
        }

        public IList<EntityNomVO> GetBudgetPeriodNoms(int programmePriorityId, string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<BudgetPeriod>()
                .AndStringContains(this.nameSelector, term);

            var usedBudgetPeriods = from ppb in this.unitOfWork.DbContext.Set<MapNodeBudget>()
                                    where ppb.MapNodeId == programmePriorityId
                                    select ppb.BudgetPeriodId;

            var budgetPeriods = from bp in this.unitOfWork.DbContext.Set<BudgetPeriod>().Where(predicate)
                                where !usedBudgetPeriods.Contains(bp.BudgetPeriodId)
                                select bp;

            return budgetPeriods
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
