using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularitySanctionProcedureKinds")]
    public class IrregularitySanctionProcedureKindNomsController : EnumNomsController<IrregularitySanctionProcedureKind>
    {
        public IrregularitySanctionProcedureKindNomsController(IEnumNomsRepository<IrregularitySanctionProcedureKind> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
