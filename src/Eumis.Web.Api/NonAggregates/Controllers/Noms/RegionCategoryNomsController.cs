using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Data;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.Indicators.ViewObjects;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.Indicators;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/regionCategories")]
    public class RegionCategoryNomsController : EnumNomsController<RegionCategory>
    {
        public RegionCategoryNomsController(IEnumNomsRepository<RegionCategory> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
