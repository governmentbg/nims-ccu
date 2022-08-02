using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/procedureEvalTypes")]
    public class ProcedureEvalTypeNomsController : EnumNomsController<ProcedureEvalType>
    {
        public ProcedureEvalTypeNomsController(IEnumNomsRepository<ProcedureEvalType> enumNomsRepository)
            : base(enumNomsRepository)
        {
        }
    }
}
