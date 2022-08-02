using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.EvalSessions;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionEvaluationCalculationTypes")]
    public class EvalSessionEvaluationCalculationTypeNomsController : EnumNomsController<EvalSessionEvaluationCalculationType>
    {
        public EvalSessionEvaluationCalculationTypeNomsController(IEnumNomsRepository<EvalSessionEvaluationCalculationType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
