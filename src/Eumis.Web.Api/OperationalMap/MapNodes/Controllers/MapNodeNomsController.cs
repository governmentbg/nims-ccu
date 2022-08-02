using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Data;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.NonAggregates.Repositories;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.OperationalMap.MapNodes.Controllers
{
    [RoutePrefix("api/nomenclatures/mapNodes")]
    public class MapNodeNomsController : EntityNomsController<MapNode, EntityNomVO>
    {
        public MapNodeNomsController(IEntityNomsRepository<MapNode, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
