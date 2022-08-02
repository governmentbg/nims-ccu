using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.UserOrganizations.Repositories;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/nomenclatures/projectProcedures")]
    public class ProjectProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        public ProjectProcedureNomsController(IProjectProcedureNomsRepository projectProcedureNomsRepository)
            : base(projectProcedureNomsRepository)
        {
        }
    }
}
