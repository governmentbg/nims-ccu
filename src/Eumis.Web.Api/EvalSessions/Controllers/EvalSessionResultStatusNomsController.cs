using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionResultStatuses")]
    public class EvalSessionResultStatusNomsController : EnumNomsController<EvalSessionResultStatus>
    {
        public EvalSessionResultStatusNomsController(IEnumNomsRepository<EvalSessionResultStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
