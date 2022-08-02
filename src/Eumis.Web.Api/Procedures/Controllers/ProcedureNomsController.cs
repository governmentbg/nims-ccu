using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/nomenclatures/procedures")]
    public class ProcedureNomsController : EntityNomsController<Procedure, EntityNomVO>
    {
        private IProcedureNomsRepository procedureNomsRepository;

        public ProcedureNomsController(IProcedureNomsRepository procedureNomsRepository)
            : base(procedureNomsRepository)
        {
            this.procedureNomsRepository = procedureNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetProcedureNoms(int programmePriorityId, string term = null, int offset = 0, int? limit = null)
        {
            return this.procedureNomsRepository.GetProcedureNomsByProgrammePriority(programmePriorityId, term, offset, limit);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetProcedureNoms(int programmeId, int programmePriorityId, string term = null, int offset = 0, int? limit = null)
        {
            return this.procedureNomsRepository.GetProcedureNomsByProgrammeAndProgrammePriority(programmeId, programmePriorityId, term, offset, limit);
        }
    }
}
