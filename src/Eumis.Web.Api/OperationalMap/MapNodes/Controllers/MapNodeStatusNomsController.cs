using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.OperationalMap.MapNodes.Controllers
{
    [RoutePrefix("api/nomenclatures/mapNodeStatuses")]
    public class MapNodeStatusNomsController : EnumNomsController<MapNodeStatus>
    {
        public MapNodeStatusNomsController(IEnumNomsRepository<MapNodeStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
