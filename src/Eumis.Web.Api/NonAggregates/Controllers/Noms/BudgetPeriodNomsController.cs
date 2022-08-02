using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Data;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.NonAggregates.Repositories;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/budgetPeriods")]
    public class BudgetPeriodNomsController : EntityNomsController<BudgetPeriod, EntityNomVO>
    {
        private IBudgetPeriodNomsRepository budgetPeriodNomsRepository;

        public BudgetPeriodNomsController(IBudgetPeriodNomsRepository budgetPeriodNomsRepository)
            : base(budgetPeriodNomsRepository)
        {
            this.budgetPeriodNomsRepository = budgetPeriodNomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetUnusedProgrammePriorityBudgetPeriods(int programmePriorityId, string term = null, int offset = 0, int? limit = null)
        {
            return this.budgetPeriodNomsRepository.GetBudgetPeriodNoms(programmePriorityId, term, offset, limit);
        }
    }
}
