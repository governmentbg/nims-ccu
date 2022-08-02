using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.FinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/financialCorrectionVersionStatuses")]
    public class FinancialCorrectionVersionStatusNomsController : EnumNomsController<FinancialCorrectionVersionStatus>
    {
        public FinancialCorrectionVersionStatusNomsController(IEnumNomsRepository<FinancialCorrectionVersionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
