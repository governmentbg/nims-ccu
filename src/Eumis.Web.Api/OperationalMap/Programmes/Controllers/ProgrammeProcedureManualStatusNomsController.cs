using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.ProcedureManuals;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/nomenclatures/programmeProcedureManualStatuses")]
    public class ProgrammeProcedureManualStatusNomsController : EnumNomsController<ProgrammeProcedureManualStatus>
    {
        public ProgrammeProcedureManualStatusNomsController(IEnumNomsRepository<ProgrammeProcedureManualStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
