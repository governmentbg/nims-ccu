using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Core;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/yesNoNonApplicable")]
    public class YesNoNonApplicableNomsController : EnumNomsController<YesNoNonApplicable>
    {
        public YesNoNonApplicableNomsController(IEnumNomsRepository<YesNoNonApplicable> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
