using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportMicroCheckStatuses")]
    public class ContractReportMicroCheckStatusNomsController : EnumNomsController<ContractReportMicroCheckStatus>
    {
        public ContractReportMicroCheckStatusNomsController(IEnumNomsRepository<ContractReportMicroCheckStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
