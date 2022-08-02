using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/nomenclatures/certAuthorityCheckAscertainmentStatuses")]
    public class CertAuthorityCheckAscertainmentStatusNomsController : EnumNomsController<CertAuthorityCheckAscertainmentStatus>
    {
        public CertAuthorityCheckAscertainmentStatusNomsController(IEnumNomsRepository<CertAuthorityCheckAscertainmentStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
