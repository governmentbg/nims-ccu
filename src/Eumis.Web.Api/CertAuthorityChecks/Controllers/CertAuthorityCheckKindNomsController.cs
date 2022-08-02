using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/nomenclatures/certAuthorityCheckKinds")]
    public class CertAuthorityCheckKindNomsController : EnumNomsController<CertAuthorityCheckKind>
    {
        public CertAuthorityCheckKindNomsController(IEnumNomsRepository<CertAuthorityCheckKind> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
