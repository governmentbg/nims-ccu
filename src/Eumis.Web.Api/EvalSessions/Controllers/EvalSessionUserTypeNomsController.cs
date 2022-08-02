using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionUserTypes")]
    public class EvalSessionUserTypeNomsController : EnumNomsController<EvalSessionUserType>
    {
        public EvalSessionUserTypeNomsController(IEnumNomsRepository<EvalSessionUserType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
