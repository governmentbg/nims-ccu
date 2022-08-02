using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Procedures.Repositories;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/massCommunicationProcedures")]
    public class ProcedureMassCommunicationProcedureNomsController : ApiController
    {
        private IProcedureNomsRepository procedureNomsRepository;

        public ProcedureMassCommunicationProcedureNomsController(
            IProcedureNomsRepository procedureNomsRepository)
        {
            this.procedureNomsRepository = procedureNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.procedureNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int programmeId = 0, string term = null, int offset = 0, int? limit = null)
        {
            return this.procedureNomsRepository.GetProcedureNoms(programmeId, term, offset, limit);
        }
    }
}
