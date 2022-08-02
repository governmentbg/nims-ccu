using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/months")]
    public class MonthNomsController : EnumNomsController<Month>
    {
        public MonthNomsController(IEnumNomsRepository<Month> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
