using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/allowedRegistrationTypes")]
    public class AllowedRegistrationTypeNomsController : EnumNomsController<AllowedRegistrationType>
    {
        public AllowedRegistrationTypeNomsController(IEnumNomsRepository<AllowedRegistrationType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
