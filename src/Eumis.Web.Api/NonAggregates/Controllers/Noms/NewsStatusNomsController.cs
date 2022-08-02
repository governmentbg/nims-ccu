using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/newsStatuses")]
    public class NewsStatusNomsController : EnumNomsController<NewsStatus>
    {
        public NewsStatusNomsController(IEnumNomsRepository<NewsStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
