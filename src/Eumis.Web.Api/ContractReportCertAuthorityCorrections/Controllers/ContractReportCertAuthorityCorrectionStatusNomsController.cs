using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportCertAuthorityCorrectionStatuses")]
    public class ContractReportCertAuthorityCorrectionStatusNomsController : EnumNomsController<ContractReportCertAuthorityCorrectionStatus>
    {
        public ContractReportCertAuthorityCorrectionStatusNomsController(IEnumNomsRepository<ContractReportCertAuthorityCorrectionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
