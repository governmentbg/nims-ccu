using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.OperationalMap.Directions.Repositories;
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
    [RoutePrefix("api/nomenclatures/subDirections")]
    public class SubDirectionNomsController : EntityNomsController<SubDirection, EntityNomVO>
    {
        private ISubDirectionNomsRepository nomsRepository;

        public SubDirectionNomsController(ISubDirectionNomsRepository nomsRepository)
            : base(nomsRepository)
        {
            this.nomsRepository = nomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetSubDirectons(int directionId, string term = null, int offset = 0, int? limit = null)
        {
            return this.nomsRepository.GetNoms(directionId, term, offset, limit);
        }
    }
}
