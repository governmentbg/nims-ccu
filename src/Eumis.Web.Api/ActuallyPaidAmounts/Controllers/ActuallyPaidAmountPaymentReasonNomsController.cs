using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ActuallyPaidAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/actuallyPaidAmountPaymentReasons")]
    public class ActuallyPaidAmountPaymentReasonNomsController : EnumNomsController<PaymentReason>
    {
        public ActuallyPaidAmountPaymentReasonNomsController(IEnumNomsRepository<PaymentReason> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
