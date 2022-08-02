using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/nomenclatures/procedureMonitorstatEconomicActivityTypes")]
    public class ProcedureMonitorstatEconomicActivityTypeNomsController : EnumNomsController<ProcedureMonitorstatEconomicActivityType>
    {
        public ProcedureMonitorstatEconomicActivityTypeNomsController(IEnumNomsRepository<ProcedureMonitorstatEconomicActivityType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
