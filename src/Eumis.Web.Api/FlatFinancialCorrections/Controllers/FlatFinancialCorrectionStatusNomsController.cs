using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/flatFinancialCorrectionStatuses")]
    public class FlatFinancialCorrectionStatusNomsController : EnumNomsController<FlatFinancialCorrectionStatus>
    {
        public FlatFinancialCorrectionStatusNomsController(IEnumNomsRepository<FlatFinancialCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
