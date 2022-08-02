using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionProjectStandingTypes")]
    public class EvalSessionProjectStandingTypeNomsController : EnumNomsController<EvalSessionProjectStandingType>
    {
        public EvalSessionProjectStandingTypeNomsController(IEnumNomsRepository<EvalSessionProjectStandingType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
