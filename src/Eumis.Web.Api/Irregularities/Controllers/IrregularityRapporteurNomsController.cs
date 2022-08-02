using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityRapporteurs")]
    public class IrregularityRapporteurNomsController : EnumNomsController<IrregularityRapporteur>
    {
        public IrregularityRapporteurNomsController(IEnumNomsRepository<IrregularityRapporteur> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
