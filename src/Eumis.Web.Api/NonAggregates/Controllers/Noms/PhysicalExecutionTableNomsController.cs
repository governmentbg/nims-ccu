using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.PhysicalExecution;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/physicalExecutionTables")]
    public class PhysicalExecutionTableNomsController : EnumNomsController<PhysicalExecutionTables>
    {
        public PhysicalExecutionTableNomsController(IEnumNomsRepository<PhysicalExecutionTables> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
