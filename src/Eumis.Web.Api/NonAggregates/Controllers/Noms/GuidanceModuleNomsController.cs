using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Guidances;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/guidanceModules")]
    public class GuidanceModuleNomsController : EnumNomsController<GuidanceModule>
    {
        public GuidanceModuleNomsController(IEnumNomsRepository<GuidanceModule> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
