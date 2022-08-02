using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.FinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/correctionBearers")]
    public class CorrectionBearerNomsController : EnumNomsController<CorrectionBearer>
    {
        public CorrectionBearerNomsController(IEnumNomsRepository<CorrectionBearer> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
