using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.RequestPackages;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/requestStatuses")]
    public class RequestStatusNomsController : EnumNomsController<RequestStatus>
    {
        public RequestStatusNomsController(IEnumNomsRepository<RequestStatus> requestStatusEnumNomsRepository)
            : base(requestStatusEnumNomsRepository)
        {
        }
    }
}
