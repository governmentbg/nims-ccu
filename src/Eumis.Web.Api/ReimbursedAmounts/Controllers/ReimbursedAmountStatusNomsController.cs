using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ReimbursedAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/reimbursedAmountStatuses")]
    public class ReimbursedAmountStatusNomsController : EnumNomsController<ReimbursedAmountStatus>
    {
        public ReimbursedAmountStatusNomsController(IEnumNomsRepository<ReimbursedAmountStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
