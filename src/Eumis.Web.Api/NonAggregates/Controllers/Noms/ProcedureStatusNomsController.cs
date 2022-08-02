using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/procedureStatuses")]
    public class ProcedureStatusNomsController : EnumNomsController<ProcedureStatus>
    {
        public ProcedureStatusNomsController(IEnumNomsRepository<ProcedureStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
