using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Data;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.Indicators.ViewObjects;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.Indicators;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Indicators.Controllers
{
    [RoutePrefix("api/nomenclatures/indicators")]
    public class IndicatorNomsController : EntityNomsController<Indicator, IndicatorNomVO>
    {
        private IIndicatorNomsRepository indicatorNomsRepository;

        public IndicatorNomsController(IIndicatorNomsRepository indicatorNomsRepository)
            : base(indicatorNomsRepository)
        {
            this.indicatorNomsRepository = indicatorNomsRepository;
        }

        [Route("")]
        public IEnumerable<IndicatorNomVO> GetUnusedMapNodeIndicators(int mapNodeId, string term = null, int offset = 0, int? limit = null)
        {
            return this.indicatorNomsRepository.GetUnusedMapNodeIndicators(mapNodeId, term, offset, limit);
        }

        [Route("")]
        public IEnumerable<IndicatorNomVO> GetUnusedProcedureIndicators(int procedureId, string term = null, int offset = 0, int? limit = null)
        {
            return this.indicatorNomsRepository.GetUnusedProcedureIndicators(procedureId, term, offset, limit);
        }
    }
}
