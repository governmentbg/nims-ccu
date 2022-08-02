using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/newsTypes")]
    public class NewsTypeNomsController : EnumNomsController<NewsType>
    {
        public NewsTypeNomsController(IEnumNomsRepository<NewsType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
