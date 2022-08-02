using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.FinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/amendmentReasons")]
    public class AmendmentReasonNomsController : EnumNomsController<AmendmentReason>
    {
        public AmendmentReasonNomsController(IEnumNomsRepository<AmendmentReason> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
