using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularitySignalStatuses")]
    public class IrregularitySignalStatusNomsController : EnumNomsController<IrregularitySignalStatus>
    {
        public IrregularitySignalStatusNomsController(IEnumNomsRepository<IrregularitySignalStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
