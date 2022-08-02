using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/protectedZones")]
    public class ProtectedZoneNomsController : EntityNomsController<ProtectedZone, NutsCodeNomVO>
    {
        private IProtectedZoneNomsRepository protectedZoneNomsRepository;

        public ProtectedZoneNomsController(IProtectedZoneNomsRepository protectedZoneNomsRepository)
            : base(protectedZoneNomsRepository)
        {
            this.protectedZoneNomsRepository = protectedZoneNomsRepository;
        }

        [Route("")]
        public IEnumerable<NutsCodeNomVO> GetProtectedZoneNoms(int countryId, string term, int offset = 0, int? limit = null)
        {
            return this.protectedZoneNomsRepository.GetProtectedZoneNoms(countryId, term, offset, limit);
        }
    }
}
