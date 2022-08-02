using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/groupingLevels")]
    public class GroupingLevelNomsController : EnumNomsController<GroupingLevel>
    {
        public GroupingLevelNomsController(IEnumNomsRepository<GroupingLevel> groupingLevelEnumNomsRepository)
            : base(groupingLevelEnumNomsRepository)
        {
        }
    }
}
