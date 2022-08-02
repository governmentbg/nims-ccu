using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportAdvanceNVPaymentAmounts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportAdvanceNVPaymentAmountStatuses")]
    public class ContractReportAdvanceNVPaymentAmountStatusNomsController : EnumNomsController<ContractReportAdvanceNVPaymentAmountStatus>
    {
        public ContractReportAdvanceNVPaymentAmountStatusNomsController(IEnumNomsRepository<ContractReportAdvanceNVPaymentAmountStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
