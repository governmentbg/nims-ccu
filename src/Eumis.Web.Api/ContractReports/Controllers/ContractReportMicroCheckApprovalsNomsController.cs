using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportMicroCheckApprovals")]
    public class ContractReportMicroCheckApprovalsNomsController : EnumNomsController<ContractReportMicroCheckApproval>
    {
        public ContractReportMicroCheckApprovalsNomsController(IEnumNomsRepository<ContractReportMicroCheckApproval> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
