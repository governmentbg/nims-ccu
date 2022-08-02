using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/procedureApplicationDocs")]
    public class ProcedureApplicationDocNomsController : EntityNomsController<ProcedureApplicationDoc, EntityNomVO>
    {
        private IProcedureApplicationDocNomsRepository procedureApplicationDocNomsRepository;

        public ProcedureApplicationDocNomsController(IProcedureApplicationDocNomsRepository procedureApplicationDocNomsRepository)
            : base(procedureApplicationDocNomsRepository)
        {
            this.procedureApplicationDocNomsRepository = procedureApplicationDocNomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetProcedureApplicationDocs(int procedureId, string term = null, int offset = 0, int? limit = null)
        {
            return this.procedureApplicationDocNomsRepository.GetProcedureApplicationDocNoms(procedureId, term, offset, limit);
        }
    }
}
