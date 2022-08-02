using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Procedures.Repositories;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/nomenclatures/proceduresByProgramme")]
    public class ProceduresByProgrammeNomsController : ApiController
    {
        private IProcedureNomsRepository procedureNomsRepository;

        public ProceduresByProgrammeNomsController(IProcedureNomsRepository procedureNomsRepository)
        {
            this.procedureNomsRepository = procedureNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int programmeId, string term = null, int offset = 0, int? limit = null)
        {
            return this.procedureNomsRepository.GetProcedureNomsByProgramme(programmeId, term, offset, limit);
        }
    }
}
