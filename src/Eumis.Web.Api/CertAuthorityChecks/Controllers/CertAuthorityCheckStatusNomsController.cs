using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/nomenclatures/certAuthorityCheckStatuses")]
    public class CertAuthorityCheckStatusNomsController : EnumNomsController<CertAuthorityCheckStatus>
    {
        public CertAuthorityCheckStatusNomsController(IEnumNomsRepository<CertAuthorityCheckStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
