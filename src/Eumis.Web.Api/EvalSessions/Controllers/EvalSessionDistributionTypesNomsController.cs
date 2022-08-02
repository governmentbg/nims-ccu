using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionDistributionTypes")]
    public class EvalSessionDistributionTypesNomsController : EnumNomsController<EvalSessionDistributionType>
    {
        public EvalSessionDistributionTypesNomsController(IEnumNomsRepository<EvalSessionDistributionType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
