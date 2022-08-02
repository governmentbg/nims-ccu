using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/settlements")]
    public class SettlementNomsController : EntityNomsController<Settlement, SettlementCodeNomVO>
    {
        private ISettlementNomsRepository settlementNomsRepository;

        public SettlementNomsController(ISettlementNomsRepository settlementNomsRepository)
            : base(settlementNomsRepository)
        {
            this.settlementNomsRepository = settlementNomsRepository;
        }

        [Route("")]
        public IEnumerable<SettlementCodeNomVO> GetSettlementNoms(int municipalityId, string term, int offset = 0, int? limit = null)
        {
            return this.settlementNomsRepository.GetSettlementNoms(municipalityId, term, offset, limit);
        }
    }
}
