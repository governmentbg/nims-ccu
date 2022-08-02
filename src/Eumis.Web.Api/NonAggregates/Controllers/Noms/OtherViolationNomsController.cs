using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/otherViolations")]
    public class OtherViolationNomsController : ApiController
    {
        private IOtherViolationNomsRepository otherViolationNomsRepository;

        public OtherViolationNomsController(IOtherViolationNomsRepository otherViolationNomsRepository)
        {
            this.otherViolationNomsRepository = otherViolationNomsRepository;
        }

        [Route("{id}")]
        public EntityNomVO GetNom(int id)
        {
            return this.otherViolationNomsRepository.GetNom(id);
        }

        [Route("")]
        public IList<EntityNomVO> GetNoms([FromUri]int[] ids, string term = null, int offset = 0, int? limit = null)
        {
            return this.otherViolationNomsRepository.GetNoms(ids, term, offset, limit);
        }
    }
}
