using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportPaymentStatuses")]
    public class ContractReportPaymentStatusNomsController : EnumNomsController<ContractReportPaymentStatus>
    {
        public ContractReportPaymentStatusNomsController(IEnumNomsRepository<ContractReportPaymentStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
