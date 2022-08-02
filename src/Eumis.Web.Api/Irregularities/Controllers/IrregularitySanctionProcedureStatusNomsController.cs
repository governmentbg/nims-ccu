using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularitySanctionProcedureStatuses")]
    public class IrregularitySanctionProcedureStatusNomsController : EnumNomsController<IrregularitySanctionProcedureStatus>
    {
        public IrregularitySanctionProcedureStatusNomsController(IEnumNomsRepository<IrregularitySanctionProcedureStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
