using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionSheetStatuses")]
    public class EvalSessionSheetStatusNomsController : ApiController
    {
        private IEvalSessionSheetStatusEnumNomsRepository nomsRepository;

        public EvalSessionSheetStatusNomsController(IEvalSessionSheetStatusEnumNomsRepository nomsRepository)
        {
            this.nomsRepository = nomsRepository;
        }

        [Route("{id}")]
        public EnumNomVO<EvalSessionSheetStatus> GetNom(EvalSessionSheetStatus id)
        {
            return this.nomsRepository.GetNom(id);
        }

        [Route("")]
        public IList<EnumNomVO<EvalSessionSheetStatus>> GetNoms([FromUri]EvalSessionSheetStatus[] ids, string term = null)
        {
            return this.nomsRepository.GetNoms(ids, term);
        }
    }
}
