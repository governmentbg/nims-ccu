using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionDistributions")]
    public class EvalSessionDistributionNomsController : ApiController
    {
        private IEvalSessionDistributionNomsRepository evalSessionDistributionNomsRepository;

        public EvalSessionDistributionNomsController(IEvalSessionDistributionNomsRepository evalSessionDistributionNomsRepository)
        {
            this.evalSessionDistributionNomsRepository = evalSessionDistributionNomsRepository;
        }

        [Route("{id}")]
        public EntityNomVO GetNom(int id, int evalSessionId)
        {
            return this.evalSessionDistributionNomsRepository.GetNom(id, evalSessionId);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int evalSessionId, string term = null, int offset = 0, int? limit = null)
        {
            return this.evalSessionDistributionNomsRepository.GetNoms(evalSessionId, term, offset, limit);
        }
    }
}
