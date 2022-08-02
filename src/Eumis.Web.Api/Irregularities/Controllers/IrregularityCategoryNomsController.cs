using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityCategories")]
    public class IrregularityCategoryNomsController : EntityNomsController<IrregularityCategory, EntityCodeNomVO>
    {
        public IrregularityCategoryNomsController(IEntityCodeNomsRepository<IrregularityCategory, EntityCodeNomVO> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
