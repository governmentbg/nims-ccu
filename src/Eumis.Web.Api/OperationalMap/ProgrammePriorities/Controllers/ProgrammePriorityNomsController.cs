using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.Controllers
{
    [RoutePrefix("api/nomenclatures/programmePriorities")]
    public class ProgrammePriorityNomsController : EntityNomsController<ProgrammePriority, EntityNomVO>
    {
        private IProgrammePriorityNomsRepository programmePriorityNomsRepository;

        public ProgrammePriorityNomsController(IProgrammePriorityNomsRepository programmePriorityNomsRepository)
            : base(programmePriorityNomsRepository)
        {
            this.programmePriorityNomsRepository = programmePriorityNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetProgrammePriorityNoms(int programmeId, string term = null, int offset = 0, int? limit = null)
        {
            return this.programmePriorityNomsRepository.GetProgrammePriorityNoms(term, offset, limit, programmeId);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetProcedureProgrammePriorityNoms(int procedureId, int programmeId, string term = null, int offset = 0, int? limit = null)
        {
            return this.programmePriorityNomsRepository.GetProcedureProgrammePriorityNoms(procedureId, programmeId, term, offset, limit);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetContractProgrammePriorityNoms(int contractId, string term = null, int offset = 0, int? limit = null)
        {
            return this.programmePriorityNomsRepository.GetContractProgrammePriorityNoms(contractId, term, offset, limit);
        }
    }
}
