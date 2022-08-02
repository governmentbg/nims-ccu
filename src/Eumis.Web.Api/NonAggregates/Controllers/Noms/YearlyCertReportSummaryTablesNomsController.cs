using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertReports;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/yearlyCertReportSummaryTables")]
    public class YearlyCertReportSummaryTablesNomsController : EnumNomsController<YearlyCertReportSummaryTables>
    {
        public YearlyCertReportSummaryTablesNomsController(IEnumNomsRepository<YearlyCertReportSummaryTables> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
