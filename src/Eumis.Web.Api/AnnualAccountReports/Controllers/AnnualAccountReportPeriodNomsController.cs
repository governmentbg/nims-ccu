using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/nomenclatures/annualAccountReportPeriods")]
    public class AnnualAccountReportPeriodNomsController : EnumNomsController<AnnualAccountReportPeriod>
    {
        public AnnualAccountReportPeriodNomsController(IEnumNomsRepository<AnnualAccountReportPeriod> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
