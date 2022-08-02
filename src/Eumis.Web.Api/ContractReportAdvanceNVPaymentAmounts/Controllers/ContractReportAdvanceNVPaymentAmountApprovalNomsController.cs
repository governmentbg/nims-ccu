using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportAdvanceNVPaymentAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportAdvanceNVPaymentAmountApprovals")]
    public class ContractReportAdvanceNVPaymentAmountApprovalNomsController : EnumNomsController<ContractReportAdvanceNVPaymentAmountApproval>
    {
        public ContractReportAdvanceNVPaymentAmountApprovalNomsController(IEnumNomsRepository<ContractReportAdvanceNVPaymentAmountApproval> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
