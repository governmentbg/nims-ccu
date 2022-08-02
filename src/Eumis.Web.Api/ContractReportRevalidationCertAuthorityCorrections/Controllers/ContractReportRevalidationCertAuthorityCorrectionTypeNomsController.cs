using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportRevalidationCertAuthorityCorrectionTypes")]
    public class ContractReportRevalidationCertAuthorityCorrectionTypeNomsController : EnumNomsController<ContractReportRevalidationCertAuthorityCorrectionType>
    {
        public ContractReportRevalidationCertAuthorityCorrectionTypeNomsController(IEnumNomsRepository<ContractReportRevalidationCertAuthorityCorrectionType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
