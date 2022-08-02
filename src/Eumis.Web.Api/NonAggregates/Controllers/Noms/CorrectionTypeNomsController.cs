using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/correctionTypes")]
    public class CorrectionTypeNomsController : EnumNomsController<CorrectionType>
    {
        public CorrectionTypeNomsController(IEnumNomsRepository<CorrectionType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
