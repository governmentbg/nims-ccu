using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/personalUinTypes")]
    public class PersonalUinTypeNomsController : EnumNomsController<PersonalUinType>
    {
        public PersonalUinTypeNomsController(IEnumNomsRepository<PersonalUinType> enumNomsRepository)
            : base(enumNomsRepository)
        {
        }
    }
}
