using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionResultTypes")]
    public class EvalSessionResultTypeNomsController : EnumNomsController<EvalSessionResultType>
    {
        public EvalSessionResultTypeNomsController(IEnumNomsRepository<EvalSessionResultType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
