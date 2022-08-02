using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/nomenclatures/certAuthorityCheckSubjectType")]
    public class CertAuthorityCheckSubjectTypeNomsController : EnumNomsController<CertAuthorityCheckSubjectType>
    {
        public CertAuthorityCheckSubjectTypeNomsController(IEnumNomsRepository<CertAuthorityCheckSubjectType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
