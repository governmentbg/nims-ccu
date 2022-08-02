using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.ActionLogs.Repositories;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Web.Api.ActionLogs.Controllers
{
    [RoutePrefix("api/nomenclatures/actionLogGroups")]
    public class ActionLogGroupNomsController : ApiController
    {
        private IActionLogGroupNomsRepository actionLogGroupNomsRepository;

        public ActionLogGroupNomsController(IActionLogGroupNomsRepository actionLogGroupNomsRepository)
        {
            this.actionLogGroupNomsRepository = actionLogGroupNomsRepository;
        }

        [Route("{id}")]
        public EntityNomVO GetNom(int id)
        {
            return this.actionLogGroupNomsRepository.GetNom(id);
        }

        [Route("")]
        public IList<EntityNomVO> GetNoms(bool procedureNomsOnly = false, string term = null, int offset = 0, int? limit = null)
        {
            return this.actionLogGroupNomsRepository.GetNoms(procedureNomsOnly, term, offset, limit);
        }
    }
}
