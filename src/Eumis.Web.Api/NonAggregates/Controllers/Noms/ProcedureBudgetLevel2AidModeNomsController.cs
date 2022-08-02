using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/procedureBudgetLevel2AidModes")]
    public class ProcedureBudgetLevel2AidModeNomsController : EnumNomsController<ProcedureBudgetLevel2AidMode>
    {
        public ProcedureBudgetLevel2AidModeNomsController(IEnumNomsRepository<ProcedureBudgetLevel2AidMode> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
