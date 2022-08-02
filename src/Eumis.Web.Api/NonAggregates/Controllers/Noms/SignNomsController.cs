using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Core;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/signs")]
    public class SignNomsController : EnumNomsController<Sign>
    {
        public SignNomsController(IEnumNomsRepository<Sign> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
