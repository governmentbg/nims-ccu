using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.RequestPackages;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/requestPackageStatuses")]
    public class RequestPackageStatusNomsController : EnumNomsController<RequestPackageStatus>
    {
        public RequestPackageStatusNomsController(IEnumNomsRepository<RequestPackageStatus> requestPackageStatusEnumNomsRepository)
            : base(requestPackageStatusEnumNomsRepository)
        {
        }
    }
}
