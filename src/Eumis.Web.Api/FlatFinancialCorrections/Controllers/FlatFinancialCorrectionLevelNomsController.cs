using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.FlatFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/flatFinancialCorrectionLevels")]
    public class FlatFinancialCorrectionLevelNomsController : EnumNomsController<FlatFinancialCorrectionLevel>
    {
        public FlatFinancialCorrectionLevelNomsController(IEnumNomsRepository<FlatFinancialCorrectionLevel> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
