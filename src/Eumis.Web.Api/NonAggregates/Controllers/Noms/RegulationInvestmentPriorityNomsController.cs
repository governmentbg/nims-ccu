using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/regulationInvestmentPriorities")]
    public class RegulationInvestmentPriorityNomsController : EntityNomsController<RegulationInvestmentPriority, EntityNomVO>
    {
        private IRegulationInvestmentPriorityNomsRepository regulationInvestmentPriorityNomsRepository;

        public RegulationInvestmentPriorityNomsController(IRegulationInvestmentPriorityNomsRepository regulationInvestmentPriorityNomsRepository)
            : base(regulationInvestmentPriorityNomsRepository)
        {
            this.regulationInvestmentPriorityNomsRepository = regulationInvestmentPriorityNomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetForInterventionCategory(int interventionCategoryId, string term = null, int offset = 0, int? limit = null)
        {
            return this.regulationInvestmentPriorityNomsRepository.GetNomsForInterventionCategory(interventionCategoryId, term, offset, limit);
        }
    }
}
