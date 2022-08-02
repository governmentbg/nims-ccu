using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityImpairedRegulationActs")]
    public class IrregularityImpairedRegulationActNomsController : EnumNomsController<IrregularityImpairedRegulationAct>
    {
        public IrregularityImpairedRegulationActNomsController(IEnumNomsRepository<IrregularityImpairedRegulationAct> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
