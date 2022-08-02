using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/nomenclatures/programmeApplicationDocuments")]
    public class ProgrammeApplicationDocumentsNomsController : EntityNomsController<ProgrammeApplicationDocument, EntityNomVO>
    {
        private IProgrammeApplicationDocumentNomsRepository programmeApplicationDocumentsNomsRepository;

        public ProgrammeApplicationDocumentsNomsController(IProgrammeApplicationDocumentNomsRepository programmeApplicationDocumentsNomsRepository)
            : base(programmeApplicationDocumentsNomsRepository)
        {
            this.programmeApplicationDocumentsNomsRepository = programmeApplicationDocumentsNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetProgrammeApplicationDocuments(int procedureId, string term = null, int offset = 0, int? limit = null)
        {
            return this.programmeApplicationDocumentsNomsRepository.GetProgrammeApplicationDocuments(procedureId, term, offset, limit);
        }
    }
}
