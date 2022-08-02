using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EuReimbursedAmounts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.EuReimbursedAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/euReimbursedAmountStatuses")]
    public class EuReimbursedAmountStatusNomsController : EnumNomsController<EuReimbursedAmountStatus>
    {
        public EuReimbursedAmountStatusNomsController(IEnumNomsRepository<EuReimbursedAmountStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
