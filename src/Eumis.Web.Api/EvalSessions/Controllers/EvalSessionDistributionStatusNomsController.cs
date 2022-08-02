using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionDistributionStatuses")]
    public class EvalSessionDistributionStatusNomsController : EnumNomsController<EvalSessionDistributionStatus>
    {
        public EvalSessionDistributionStatusNomsController(IEnumNomsRepository<EvalSessionDistributionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
