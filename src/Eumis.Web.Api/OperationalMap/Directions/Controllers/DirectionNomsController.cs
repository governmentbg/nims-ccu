using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.OperationalMap.Directions;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Directions.Controllers
{
    [RoutePrefix("api/nomenclatures/directions")]
    public class DirectionNomsController : EntityNomsController<Direction, EntityNomVO>
    {
        public DirectionNomsController(IEntityNomsRepository<Direction, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
