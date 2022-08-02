using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityVersionStatuses")]
    public class IrregularityVersionStatusNomsController : EnumNomsController<IrregularityVersionStatus>
    {
        public IrregularityVersionStatusNomsController(IEnumNomsRepository<IrregularityVersionStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
