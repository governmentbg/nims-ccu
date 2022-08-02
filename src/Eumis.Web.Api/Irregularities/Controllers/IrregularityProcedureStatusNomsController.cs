using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularityProcedureStatuses")]
    public class IrregularityProcedureStatusNomsController : EnumNomsController<IrregularityProcedureStatus>
    {
        public IrregularityProcedureStatusNomsController(IEnumNomsRepository<IrregularityProcedureStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
