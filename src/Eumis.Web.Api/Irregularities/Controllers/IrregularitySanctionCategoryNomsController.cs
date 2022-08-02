using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularitySanctionCategories")]
    public class IrregularitySanctionCategoryNomsController : EntityNomsController<IrregularitySanctionCategory, EntityCodeNomVO>
    {
        public IrregularitySanctionCategoryNomsController(IEntityCodeNomsRepository<IrregularitySanctionCategory, EntityCodeNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
