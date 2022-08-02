using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityCaseStates")]
    public class IrregularityCaseStateNomsController : EnumNomsController<IrregularityCaseState>
    {
        public IrregularityCaseStateNomsController(IEnumNomsRepository<IrregularityCaseState> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
