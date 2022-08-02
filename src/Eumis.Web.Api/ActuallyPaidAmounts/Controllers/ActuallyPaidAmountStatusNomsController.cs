using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ActuallyPaidAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/actuallyPaidAmountStatuses")]
    public class ActuallyPaidAmountStatusNomsController : EnumNomsController<ActuallyPaidAmountStatus>
    {
        public ActuallyPaidAmountStatusNomsController(IEnumNomsRepository<ActuallyPaidAmountStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
