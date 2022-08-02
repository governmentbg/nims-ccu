using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/nomenclatures/procedureMonitorstatRequestStatus")]
    public class ProcedureMonitorstatRequestStatusNomsController : EnumNomsController<ProcedureMonitorstatRequestStatus>
    {
        public ProcedureMonitorstatRequestStatusNomsController(IEnumNomsRepository<ProcedureMonitorstatRequestStatus> nomsRepository)
            : base(nomsRepository)
        {
        }
    }
}
