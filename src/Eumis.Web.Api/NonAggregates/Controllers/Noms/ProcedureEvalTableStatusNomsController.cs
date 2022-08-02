using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/procedureEvalTableStatuses")]
    public class ProcedureEvalTableStatusNomsController : EnumNomsController<ProcedureEvalTableStatus>
    {
        public ProcedureEvalTableStatusNomsController(IEnumNomsRepository<ProcedureEvalTableStatus> enumNomsRepository)
            : base(enumNomsRepository)
        {
        }
    }
}
