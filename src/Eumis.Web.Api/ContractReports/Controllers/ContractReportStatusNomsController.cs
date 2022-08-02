using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportStatuses")]
    public class ContractReportStatusNomsController : EnumNomsController<ContractReportStatus>
    {
        public ContractReportStatusNomsController(IEnumNomsRepository<ContractReportStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
