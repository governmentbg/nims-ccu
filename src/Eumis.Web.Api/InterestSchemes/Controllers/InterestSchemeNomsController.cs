using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.InterestSchemes;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.InterestSchemes.Controllers
{
    [RoutePrefix("api/nomenclatures/interestSchemes")]
    public class InterestSchemeNomsController : EntityNomsController<InterestScheme, EntityNomVO>
    {
        public InterestSchemeNomsController(IEntityNomsRepository<InterestScheme, EntityNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
