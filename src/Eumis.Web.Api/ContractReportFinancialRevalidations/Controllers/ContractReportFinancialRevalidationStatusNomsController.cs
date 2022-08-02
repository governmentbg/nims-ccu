using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialRevalidations.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialRevalidationStatuses")]
    public class ContractReportFinancialRevalidationStatusNomsController : EnumNomsController<ContractReportFinancialRevalidationStatus>
    {
        public ContractReportFinancialRevalidationStatusNomsController(IEnumNomsRepository<ContractReportFinancialRevalidationStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
