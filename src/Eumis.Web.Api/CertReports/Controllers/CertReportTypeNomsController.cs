using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertReports;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/nomenclatures/certReportTypes")]
    public class CertReportTypeNomsController : EnumNomsController<CertReportType>
    {
        public CertReportTypeNomsController(IEnumNomsRepository<CertReportType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
