using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportPaymentCheckApprovals")]
    public class ContractReportPaymentCheckApprovalsNomsController : EnumNomsController<ContractReportPaymentCheckApproval>
    {
        public ContractReportPaymentCheckApprovalsNomsController(IEnumNomsRepository<ContractReportPaymentCheckApproval> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
