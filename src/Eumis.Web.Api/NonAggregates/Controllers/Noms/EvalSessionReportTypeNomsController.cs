using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/evalSessionReportTypes")]
    public class EvalSessionReportTypeNomsController : EnumNomsController<EvalSessionReportType>
    {
        public EvalSessionReportTypeNomsController(IEnumNomsRepository<EvalSessionReportType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
