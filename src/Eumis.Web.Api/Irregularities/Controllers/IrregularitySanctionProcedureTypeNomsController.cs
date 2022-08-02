using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Irregularities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Irregularities.Controllers
{
    [RoutePrefix("api/nomenclatures/irregularitySanctionProcedureTypes")]
    public class IrregularitySanctionProcedureTypeNomsController : EnumNomsController<IrregularitySanctionProcedureType>
    {
        public IrregularitySanctionProcedureTypeNomsController(IEnumNomsRepository<IrregularitySanctionProcedureType> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
