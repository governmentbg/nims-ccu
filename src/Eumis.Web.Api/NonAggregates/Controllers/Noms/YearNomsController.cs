using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/years")]
    public class YearNomsController : EnumNomsController<Year>
    {
        public YearNomsController(IEnumNomsRepository<Year> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
