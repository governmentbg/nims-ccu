using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportIndicators.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportIndicatorStatuses")]
    public class ContractReportIndicatorStatusNomsController : EnumNomsController<ContractReportIndicatorStatus>
    {
        public ContractReportIndicatorStatusNomsController(IEnumNomsRepository<ContractReportIndicatorStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
