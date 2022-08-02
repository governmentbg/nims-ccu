using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Data;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.Indicators.ViewObjects;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.BasicInterestRates;
using Eumis.Domain.Indicators;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Indicators.Controllers
{
    [RoutePrefix("api/nomenclatures/indicatorItemTypes")]
    public class IndicatorItemTypeNomsController : EntityNomsController<IndicatorItemType, EntityNomVO>
    {
        public IndicatorItemTypeNomsController(IEntityNomsRepository<IndicatorItemType, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
