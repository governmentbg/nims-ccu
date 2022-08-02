using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportRevalidationCertAuthorityFinancialCorrectionStatuses")]
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionStatusNomsController : EnumNomsController<ContractReportRevalidationCertAuthorityFinancialCorrectionStatus>
    {
        public ContractReportRevalidationCertAuthorityFinancialCorrectionStatusNomsController(IEnumNomsRepository<ContractReportRevalidationCertAuthorityFinancialCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
