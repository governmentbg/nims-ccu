using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/municipalities")]
    public class MunicipalityNomsController : EntityNomsController<Municipality, NutsCodeNomVO>
    {
        private IMunicipalityNomsRepository municipalityNomsRepository;

        public MunicipalityNomsController(IMunicipalityNomsRepository municipalityNomsRepository)
            : base(municipalityNomsRepository)
        {
            this.municipalityNomsRepository = municipalityNomsRepository;
        }

        [Route("")]
        public IEnumerable<NutsCodeNomVO> GetMunicipalityNoms(int districtId, string term, int offset = 0, int? limit = null)
        {
            return this.municipalityNomsRepository.GetMunicipalityNoms(districtId, term, offset, limit);
        }
    }
}
