using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.FinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/financialCorrectionVersionViolationFoundBy")]
    public class FinancialCorrectionViolationFoundByNomsController : EnumNomsController<FinancialCorrectionVersionViolationFoundBy>
    {
        public FinancialCorrectionViolationFoundByNomsController(IEnumNomsRepository<FinancialCorrectionVersionViolationFoundBy> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
