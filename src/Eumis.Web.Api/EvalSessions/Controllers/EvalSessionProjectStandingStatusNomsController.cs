using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionProjectStandingStatuses")]
    public class EvalSessionProjectStandingStatusNomsController : EnumNomsController<EvalSessionProjectStandingStatus>
    {
        public EvalSessionProjectStandingStatusNomsController(IEnumNomsRepository<EvalSessionProjectStandingStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
