using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertReports;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/nomenclatures/certReports")]
    public class CertReportNomsController : EntityNomsController<CertReport, EntityNomVO>
    {
        public CertReportNomsController(IEntityNomsRepository<CertReport, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
