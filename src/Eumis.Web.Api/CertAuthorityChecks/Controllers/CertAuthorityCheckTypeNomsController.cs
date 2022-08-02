using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/nomenclatures/certAuthorityCheckTypes")]
    public class CertAuthorityCheckTypeNomsController : EnumNomsController<CertAuthorityCheckType>
    {
        public CertAuthorityCheckTypeNomsController(IEnumNomsRepository<CertAuthorityCheckType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
