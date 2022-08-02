using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.RequestPackages;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/requestPackageTypes")]
    public class RequestPackageTypeNomsController : EnumNomsController<RequestPackageType>
    {
        public RequestPackageTypeNomsController(IEnumNomsRepository<RequestPackageType> requestPackageTypeEnumNomsRepository)
            : base(requestPackageTypeEnumNomsRepository)
        {
        }
    }
}
