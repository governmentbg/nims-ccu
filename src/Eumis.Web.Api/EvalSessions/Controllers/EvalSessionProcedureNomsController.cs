using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.UserOrganizations.Controllers
{
    [RoutePrefix("api/nomenclatures/evalSessionProcedures")]
    public class EvalSessionProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        public EvalSessionProcedureNomsController(IEvalSessionProcedureNomsRepository evalSessionProcedureNomsRepository)
            : base(evalSessionProcedureNomsRepository)
        {
        }
    }
}
