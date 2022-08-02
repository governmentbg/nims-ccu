using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.EvalSessions;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionAssistantAssessors")]
    public class EvalSessionAssistantAssessorNomsController : ApiController
    {
        private IEvalSessionUserTypeNomsRepository evalSessionUserTypeNomsRepository;

        public EvalSessionAssistantAssessorNomsController(IEvalSessionUserTypeNomsRepository evalSessionUserTypeNomsRepository)
        {
            this.evalSessionUserTypeNomsRepository = evalSessionUserTypeNomsRepository;
        }

        [Route("{id}")]
        public EntityNomVO GetNom(int id, int evalSessionId)
        {
            return this.evalSessionUserTypeNomsRepository.GetNom(id, evalSessionId);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int evalSessionId, string term = null, int offset = 0, int? limit = null)
        {
            return this.evalSessionUserTypeNomsRepository.GetNoms(evalSessionId, EvalSessionUserType.AssistantAssessor, term, offset, limit);
        }
    }
}
