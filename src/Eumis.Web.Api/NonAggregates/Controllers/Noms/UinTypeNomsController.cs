using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/uinTypes")]
    public class UinTypeNomsController : EnumNomsController<UinType>
    {
        public UinTypeNomsController(IEnumNomsRepository<UinType> uinTypeEnumNomsRepository)
            : base(uinTypeEnumNomsRepository)
        {
        }
    }
}
