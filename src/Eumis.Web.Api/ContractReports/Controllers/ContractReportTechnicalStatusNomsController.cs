using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportTechnicalStatuses")]
    public class ContractReportTechnicalStatusNomsController : EnumNomsController<ContractReportTechnicalStatus>
    {
        public ContractReportTechnicalStatusNomsController(IEnumNomsRepository<ContractReportTechnicalStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
