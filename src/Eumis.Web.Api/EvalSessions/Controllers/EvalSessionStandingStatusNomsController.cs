using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionStandingStatuses")]
    public class EvalSessionStandingStatusNomsController : EnumNomsController<EvalSessionStandingStatus>
    {
        public EvalSessionStandingStatusNomsController(IEnumNomsRepository<EvalSessionStandingStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
