using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityClassifications")]
    public class IrregularityClassificationNomsController : EnumNomsController<IrregularityClassification>
    {
        public IrregularityClassificationNomsController(IEnumNomsRepository<IrregularityClassification> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
