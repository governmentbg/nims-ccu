using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ReimbursedAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/reimbursements")]
    public class ReimbursementNomsController : EnumNomsController<Reimbursement>
    {
        public ReimbursementNomsController(IEnumNomsRepository<Reimbursement> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
