using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/financialCorrectionImposingReasons")]
    public class FinancialCorrectionImposingReasonNomsController : EntityNomsController<FinancialCorrectionImposingReason, EntityNomVO>
    {
        public FinancialCorrectionImposingReasonNomsController(IEntityNomsRepository<FinancialCorrectionImposingReason, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
