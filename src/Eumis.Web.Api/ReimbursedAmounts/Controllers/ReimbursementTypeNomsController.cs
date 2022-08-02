using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ReimbursedAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/reimbursementTypes")]
    public class ReimbursementTypeNomsController : EnumNomsController<ReimbursementType>
    {
        public ReimbursementTypeNomsController(IEnumNomsRepository<ReimbursementType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
