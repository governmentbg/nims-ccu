using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Prognoses.Controllers
{
    [RoutePrefix("api/nomenclatures/prognosisStatuses")]
    public class PrognosisStatusNomsController : EnumNomsController<PrognosisStatus>
    {
        public PrognosisStatusNomsController(IEnumNomsRepository<PrognosisStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
