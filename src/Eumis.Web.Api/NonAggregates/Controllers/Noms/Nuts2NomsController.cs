using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.NonAggregates.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/nuts2")]
    public class Nuts2NomsController : EntityNomsController<Nuts2, NutsCodeNomVO>
    {
        private INuts2NomsRepository nuts2NomsRepository;

        public Nuts2NomsController(INuts2NomsRepository nuts2NomsRepository)
            : base(nuts2NomsRepository)
        {
            this.nuts2NomsRepository = nuts2NomsRepository;
        }

        [Route("")]
        public IEnumerable<NutsCodeNomVO> GetNuts2Noms(int nuts1Id, string term, int offset = 0, int? limit = null)
        {
            return this.nuts2NomsRepository.GetNuts2Noms(nuts1Id, term, offset, limit);
        }
    }
}
