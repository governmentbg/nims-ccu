using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityReasonsNotReportingToOlaf")]
    public class IrregularityReasonNotReportingToOlafNomsController : EnumNomsController<IrregularityReasonNotReportingToOlaf>
    {
        public IrregularityReasonNotReportingToOlafNomsController(IEnumNomsRepository<IrregularityReasonNotReportingToOlaf> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
