using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/nomenclatures/annualAccountReportStatuses")]
    public class AnnualAccountReportStatusNomsController : EnumNomsController<AnnualAccountReportStatus>
    {
        public AnnualAccountReportStatusNomsController(IEnumNomsRepository<AnnualAccountReportStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
