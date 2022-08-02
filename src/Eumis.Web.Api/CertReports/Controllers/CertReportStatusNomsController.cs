using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertReports;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/nomenclatures/certReportStatuses")]
    public class CertReportStatusNomsController : EnumNomsController<CertReportStatus>
    {
        public CertReportStatusNomsController(IEnumNomsRepository<CertReportStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
