using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/quarters")]
    public class QuarterNomsController : EnumNomsController<Quarter>
    {
        public QuarterNomsController(IEnumNomsRepository<Quarter> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
