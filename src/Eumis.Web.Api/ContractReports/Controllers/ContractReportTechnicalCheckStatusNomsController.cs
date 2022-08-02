using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportTechnicalCheckStatuses")]
    public class ContractReportTechnicalCheckStatusNomsController : EnumNomsController<ContractReportTechnicalCheckStatus>
    {
        public ContractReportTechnicalCheckStatusNomsController(IEnumNomsRepository<ContractReportTechnicalCheckStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
