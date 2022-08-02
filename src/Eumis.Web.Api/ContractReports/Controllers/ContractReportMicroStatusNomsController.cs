using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.ContractReports.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportMicroStatuses")]
    public class ContractReportMicroStatusNomsController : EnumNomsController<ContractReportMicroStatus>
    {
        public ContractReportMicroStatusNomsController(IEnumNomsRepository<ContractReportMicroStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
