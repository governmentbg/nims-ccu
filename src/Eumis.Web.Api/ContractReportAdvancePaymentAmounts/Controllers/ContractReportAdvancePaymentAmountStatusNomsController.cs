using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportAdvancePaymentAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportAdvancePaymentAmountStatuses")]
    public class ContractReportAdvancePaymentAmountStatusNomsController : EnumNomsController<ContractReportAdvancePaymentAmountStatus>
    {
        public ContractReportAdvancePaymentAmountStatusNomsController(IEnumNomsRepository<ContractReportAdvancePaymentAmountStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
