using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.CertAuthorityChecks;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CertAuthorityChecks.Controllers
{
    [RoutePrefix("api/nomenclatures/certAuthorityCheckAscertainmentExecutionStatuses")]
    public class CertAuthorityCheckAscertainmentExecutionStatusNomsController : EnumNomsController<CertAuthorityCheckAscertainmentExecutionStatus>
    {
        public CertAuthorityCheckAscertainmentExecutionStatusNomsController(IEnumNomsRepository<CertAuthorityCheckAscertainmentExecutionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
