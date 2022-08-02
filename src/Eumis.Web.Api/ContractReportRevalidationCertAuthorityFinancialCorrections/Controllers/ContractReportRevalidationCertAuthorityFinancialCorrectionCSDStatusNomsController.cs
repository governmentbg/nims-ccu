using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportRevalidationCertAuthorityFinancialCorrectionCSDStatuses")]
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatusNomsController : EnumNomsController<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus>
    {
        public ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatusNomsController(IEnumNomsRepository<ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
