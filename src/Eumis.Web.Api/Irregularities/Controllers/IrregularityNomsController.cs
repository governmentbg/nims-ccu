using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Irregularities.Repositories;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularities")]
    public class IrregularityNomsController : ApiController
    {
        private IIrregularityNomsRepository irregularityNomsRepository;

        public IrregularityNomsController(
            IIrregularityNomsRepository irregularityNomsRepository)
        {
            this.irregularityNomsRepository = irregularityNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.irregularityNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int contractId, string term = null, int offset = 0, int? limit = null)
        {
            return this.irregularityNomsRepository.GetNoms(contractId, term, offset, limit);
        }
    }
}
