using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialCheckApprovals")]
    public class ContractReportFinancialCheckApprovalsNomsController : EnumNomsController<ContractReportFinancialCheckApproval>
    {
        public ContractReportFinancialCheckApprovalsNomsController(IEnumNomsRepository<ContractReportFinancialCheckApproval> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
