using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportAdvancePaymentAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportAdvancePaymentAmountApprovals")]
    public class ContractReportAdvancePaymentAmountApprovalNomsController : EnumNomsController<ContractReportAdvancePaymentAmountApproval>
    {
        public ContractReportAdvancePaymentAmountApprovalNomsController(IEnumNomsRepository<ContractReportAdvancePaymentAmountApproval> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
