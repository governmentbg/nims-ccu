using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportIndicatorApprovals")]
    public class ContractReportIndicatorApprovalNomsController : EnumNomsController<ContractReportIndicatorApproval>
    {
        public ContractReportIndicatorApprovalNomsController(IEnumNomsRepository<ContractReportIndicatorApproval> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
