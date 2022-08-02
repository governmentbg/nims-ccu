using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityStatuses")]
    public class IrregularityStatusNomsController : EnumNomsController<IrregularityStatus>
    {
        public IrregularityStatusNomsController(IEnumNomsRepository<IrregularityStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
