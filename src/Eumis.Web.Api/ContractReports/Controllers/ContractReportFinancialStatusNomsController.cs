using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialStatuses")]
    public class ContractReportFinancialStatusNomsController : EnumNomsController<ContractReportFinancialStatus>
    {
        public ContractReportFinancialStatusNomsController(IEnumNomsRepository<ContractReportFinancialStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
