using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/nomenclatures/certAuthorityCheckAscertainmentTypes")]
    public class CertAuthorityCheckAscertainmentTypeNomsController : EnumNomsController<CertAuthorityCheckAscertainmentType>
    {
        public CertAuthorityCheckAscertainmentTypeNomsController(IEnumNomsRepository<CertAuthorityCheckAscertainmentType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
