using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/nomenclatures/certAuthorityCheckLevels")]
    public class CertAuthorityCheckLevelNomsController : EnumNomsController<CertAuthorityCheckLevel>
    {
        public CertAuthorityCheckLevelNomsController(IEnumNomsRepository<CertAuthorityCheckLevel> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
