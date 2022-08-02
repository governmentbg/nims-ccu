using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.Measures;
using Eumis.Domain.Users;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Users.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionUsers")]
    public class EvalSessionUserNomsController : EntityNomsController<User, EntityNomVO>
    {
        private IEvalSessionUserNomsRepository evalSessionUserNomsRepository;

        public EvalSessionUserNomsController(IEvalSessionUserNomsRepository evalSessionUserNomsRepository)
            : base(evalSessionUserNomsRepository)
        {
            this.evalSessionUserNomsRepository = evalSessionUserNomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetUnusedEvalSessionUsers(int evalSessionId, string term = null, int offset = 0, int? limit = null)
        {
            return this.evalSessionUserNomsRepository.GetSessionUserNoms(evalSessionId, term, offset, limit);
        }
    }
}
