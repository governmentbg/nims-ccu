using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportRevalidationCertAuthorityCorrectionStatuses")]
    public class ContractReportRevalidationCertAuthorityCorrectionStatusNomsController : EnumNomsController<ContractReportRevalidationCertAuthorityCorrectionStatus>
    {
        public ContractReportRevalidationCertAuthorityCorrectionStatusNomsController(IEnumNomsRepository<ContractReportRevalidationCertAuthorityCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
