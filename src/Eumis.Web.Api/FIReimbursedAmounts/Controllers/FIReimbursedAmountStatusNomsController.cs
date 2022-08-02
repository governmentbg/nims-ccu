using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.FIReimbursedAmounts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ReimbursedAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/fiReimbursedAmountStatuses")]
    public class FIReimbursedAmountStatusNomsController : EnumNomsController<FIReimbursedAmountStatus>
    {
        public FIReimbursedAmountStatusNomsController(IEnumNomsRepository<FIReimbursedAmountStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
