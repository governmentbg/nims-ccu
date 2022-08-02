using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertReports;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/intermediateFinalCertReportSummaryTables")]
    public class IntermediateFinalCertReportSummaryTablesNomsController : EnumNomsController<IntermediateFinalCertReportSummaryTables>
    {
        public IntermediateFinalCertReportSummaryTablesNomsController(IEnumNomsRepository<IntermediateFinalCertReportSummaryTables> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
