using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportCertAuthorityFinancialCorrectionCSDStatuses")]
    public class ContractReportCertAuthorityFinancialCorrectionCSDStatusNomsController : EnumNomsController<ContractReportCertAuthorityFinancialCorrectionCSDStatus>
    {
        public ContractReportCertAuthorityFinancialCorrectionCSDStatusNomsController(IEnumNomsRepository<ContractReportCertAuthorityFinancialCorrectionCSDStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
