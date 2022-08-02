using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/declarationStatuses")]
    public class DeclarationStatusNomsController : EnumNomsController<DeclarationStatus>
    {
        public DeclarationStatusNomsController(IEnumNomsRepository<DeclarationStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
