using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportPaymentCheckStatuses")]
    public class ContractReportPaymentCheckStatusNomsController : EnumNomsController<ContractReportPaymentCheckStatus>
    {
        public ContractReportPaymentCheckStatusNomsController(IEnumNomsRepository<ContractReportPaymentCheckStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
