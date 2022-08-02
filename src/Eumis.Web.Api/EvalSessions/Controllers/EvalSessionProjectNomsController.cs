using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionProjects")]
    public class EvalSessionProjectNomsController : ApiController
    {
        private IEvalSessionProjectNomsRepository evalSessionProjectNomsRepository;

        public EvalSessionProjectNomsController(IEvalSessionProjectNomsRepository evalSessionProjectNomsRepository)
        {
            this.evalSessionProjectNomsRepository = evalSessionProjectNomsRepository;
        }

        [Route("{id}")]
        public EntityNomVO GetNom(int id, int evalSessionId)
        {
            return this.evalSessionProjectNomsRepository.GetNom(id, evalSessionId);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int evalSessionId, string term = null, int offset = 0, int? limit = null, bool? notDeletedOnly = true)
        {
            return this.evalSessionProjectNomsRepository.GetNoms(evalSessionId, term, offset, limit, notDeletedOnly);
        }
    }
}
