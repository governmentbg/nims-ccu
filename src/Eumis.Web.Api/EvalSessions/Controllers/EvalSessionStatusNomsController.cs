using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionStatuses")]
    public class EvalSessionStatusNomsController : EnumNomsController<EvalSessionStatus>
    {
        public EvalSessionStatusNomsController(IEnumNomsRepository<EvalSessionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
