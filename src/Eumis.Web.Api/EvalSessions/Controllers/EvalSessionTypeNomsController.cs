using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionTypes")]
    public class EvalSessionTypeNomsController : EnumNomsController<EvalSessionType>
    {
        public EvalSessionTypeNomsController(IEnumNomsRepository<EvalSessionType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
