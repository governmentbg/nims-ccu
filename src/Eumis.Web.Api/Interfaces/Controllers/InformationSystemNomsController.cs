using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.Interfaces.Controllers
{
    [RoutePrefix("api/nomenclatures/informationSystems")]
    public class InformationSystemNomsController : EnumNomsController<InformationSystem>
    {
        public InformationSystemNomsController(IEnumNomsRepository<InformationSystem> informationSystemNomsRepository)
            : base(informationSystemNomsRepository)
        {
        }
    }
}
