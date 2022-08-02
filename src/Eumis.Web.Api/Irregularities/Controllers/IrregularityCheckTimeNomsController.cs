using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityCheckTime")]
    public class IrregularityCheckTimeNomsController : EnumNomsController<IrregularityCheckTime>
    {
        public IrregularityCheckTimeNomsController(IEnumNomsRepository<IrregularityCheckTime> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
