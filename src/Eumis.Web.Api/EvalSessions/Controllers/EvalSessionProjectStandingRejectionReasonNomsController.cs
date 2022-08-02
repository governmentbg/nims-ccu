using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/evalSessionProjectStandingRejectionReasons")]
    public class EvalSessionProjectStandingRejectionReasonNomsController : EntityNomsController<EvalSessionProjectStandingRejectionReason, EntityNomVO>
    {
        public EvalSessionProjectStandingRejectionReasonNomsController(IEntityNomsRepository<EvalSessionProjectStandingRejectionReason, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
