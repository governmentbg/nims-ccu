using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/districts")]
    public class DistrictNomsController : EntityNomsController<District, NutsCodeNomVO>
    {
        private IDistrictNomsRepository districtNomsRepository;

        public DistrictNomsController(IDistrictNomsRepository districtNomsRepository)
            : base(districtNomsRepository)
        {
            this.districtNomsRepository = districtNomsRepository;
        }

        [Route("")]
        public IEnumerable<NutsCodeNomVO> GetDistrictNoms(int nuts2Id, string term, int offset = 0, int? limit = null)
        {
            return this.districtNomsRepository.GetDistrictNoms(nuts2Id, term, offset, limit);
        }
    }
}
