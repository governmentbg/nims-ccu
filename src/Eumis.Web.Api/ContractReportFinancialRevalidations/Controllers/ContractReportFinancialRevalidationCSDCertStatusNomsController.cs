using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportFinancialRevalidations.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportFinancialRevalidationCSDCertStatuses")]
    public class ContractReportFinancialRevalidationCSDCertStatusNomsController : EnumNomsController<ContractReportFinancialRevalidationCSDCertStatus>
    {
        public ContractReportFinancialRevalidationCSDCertStatusNomsController(IEnumNomsRepository<ContractReportFinancialRevalidationCSDCertStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
