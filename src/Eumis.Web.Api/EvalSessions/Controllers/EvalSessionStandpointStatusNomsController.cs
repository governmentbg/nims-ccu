using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.EvalSessions;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionStandpointStatuses")]
    public class EvalSessionStandpointStatusNomsController : ApiController
    {
        private IEvalSessionStandpointStatusEnumNomsRepository nomsRepository;

        public EvalSessionStandpointStatusNomsController(IEvalSessionStandpointStatusEnumNomsRepository nomsRepository)
        {
            this.nomsRepository = nomsRepository;
        }

        [Route("{id}")]
        public EnumNomVO<EvalSessionStandpointStatus> GetNom(EvalSessionStandpointStatus id)
        {
            return this.nomsRepository.GetNom(id);
        }

        [Route("")]
        public IList<EnumNomVO<EvalSessionStandpointStatus>> GetNoms([FromUri]EvalSessionStandpointStatus[] ids, string term = null)
        {
            return this.nomsRepository.GetNoms(ids, term);
        }
    }
}
