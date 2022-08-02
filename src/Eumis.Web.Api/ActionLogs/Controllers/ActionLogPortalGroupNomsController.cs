using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.ActionLogs.Repositories;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Web.Api.ActionLogs.Controllers
{
    [RoutePrefix("api/nomenclatures/actionLogPortalGroups")]
    public class ActionLogPortalGroupNomsController : ApiController
    {
        private IActionLogPortalGroupNomsRepository actionLogPortalGroupNomsRepository;

        public ActionLogPortalGroupNomsController(IActionLogPortalGroupNomsRepository actionLogPortalGroupNomsRepository)
        {
            this.actionLogPortalGroupNomsRepository = actionLogPortalGroupNomsRepository;
        }

        [Route("{id}")]
        public EntityNomVO GetNom(int id)
        {
            return this.actionLogPortalGroupNomsRepository.GetNom(id);
        }

        [Route("")]
        public IList<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            return this.actionLogPortalGroupNomsRepository.GetNoms(term, offset, limit);
        }
    }
}
