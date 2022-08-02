using Eumis.Data.Contracts.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportStatusesWithoutDraft")]
    public class ContractReportStatusWithoutDraftNomsController : EnumNomsController<ContractReportStatus>
    {
        public ContractReportStatusWithoutDraftNomsController(IContractReportStatusWithoutDraftNomsRepository nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
