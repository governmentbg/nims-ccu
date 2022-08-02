using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportTechnicalCheckApprovals")]
    public class ContractReportTechnicalCheckApprovalsNomsController : EnumNomsController<ContractReportTechnicalCheckApproval>
    {
        public ContractReportTechnicalCheckApprovalsNomsController(IEnumNomsRepository<ContractReportTechnicalCheckApproval> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
