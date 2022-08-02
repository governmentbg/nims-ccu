using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/nuts1")]
    public class Nuts1NomsController : EntityNomsController<Nuts1, NutsCodeNomVO>
    {
        private INuts1NomsRepository nuts1NomsRepository;

        public Nuts1NomsController(INuts1NomsRepository nuts1NomsRepository)
            : base(nuts1NomsRepository)
        {
            this.nuts1NomsRepository = nuts1NomsRepository;
        }

        [Route("")]
        public IEnumerable<NutsCodeNomVO> GetNuts1Noms(int countryId, string term, int offset = 0, int? limit = null)
        {
            return this.nuts1NomsRepository.GetNuts1Noms(countryId, term, offset, limit);
        }
    }
}
