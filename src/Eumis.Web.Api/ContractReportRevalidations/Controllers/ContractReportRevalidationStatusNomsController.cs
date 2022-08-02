using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidations.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportRevalidationStatuses")]
    public class ContractReportRevalidationStatusNomsController : EnumNomsController<ContractReportRevalidationStatus>
    {
        public ContractReportRevalidationStatusNomsController(IEnumNomsRepository<ContractReportRevalidationStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
