using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EuReimbursedAmounts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.EuReimbursedAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/euReimbursedAmountPaymentTypes")]
    public class EuReimbursedAmountPaymentTypeNomsController : EnumNomsController<EuReimbursedAmountPaymentType>
    {
        public EuReimbursedAmountPaymentTypeNomsController(IEnumNomsRepository<EuReimbursedAmountPaymentType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
