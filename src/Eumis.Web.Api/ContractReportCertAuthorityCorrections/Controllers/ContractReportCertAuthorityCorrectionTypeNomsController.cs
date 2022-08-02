using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportCertAuthorityCorrectionTypes")]
    public class ContractReportCertAuthorityCorrectionTypeNomsController : EnumNomsController<ContractReportCertAuthorityCorrectionType>
    {
        public ContractReportCertAuthorityCorrectionTypeNomsController(IEnumNomsRepository<ContractReportCertAuthorityCorrectionType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
