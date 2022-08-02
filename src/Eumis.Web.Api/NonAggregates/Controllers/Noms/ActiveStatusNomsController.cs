using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/activeStatuses")]
    public class ActiveStatusNomsController : EnumNomsController<ActiveStatus>
    {
        public ActiveStatusNomsController(IEnumNomsRepository<ActiveStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
