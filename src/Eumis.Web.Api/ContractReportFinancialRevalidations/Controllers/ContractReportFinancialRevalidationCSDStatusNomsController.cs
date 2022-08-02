using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialRevalidations.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialRevalidationCSDStatuses")]
    public class ContractReportFinancialRevalidationCSDStatusNomsController : EnumNomsController<ContractReportFinancialRevalidationCSDStatus>
    {
        public ContractReportFinancialRevalidationCSDStatusNomsController(IEnumNomsRepository<ContractReportFinancialRevalidationCSDStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
