using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertAuthorityFinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportCertAuthorityFinancialCorrectionStatuses")]
    public class ContractReportCertAuthorityFinancialCorrectionStatusNomsController : EnumNomsController<ContractReportCertAuthorityFinancialCorrectionStatus>
    {
        public ContractReportCertAuthorityFinancialCorrectionStatusNomsController(IEnumNomsRepository<ContractReportCertAuthorityFinancialCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
