using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.NonAggregates.Repositories.Noms;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/procedureMonitorstatRequests")]
    public class ProcedureMonitorstatRequestNomsController : EntityNomsController<ProcedureMonitorstatRequest, EntityNomVO>
    {
        private IProcedureMonitorstatRequestNomsRepository procedureMonitorstatRequestNomsRepository;

        public ProcedureMonitorstatRequestNomsController(IProcedureMonitorstatRequestNomsRepository procedureMonitorstatRequestNomsRepository)
            : base(procedureMonitorstatRequestNomsRepository)
        {
            this.procedureMonitorstatRequestNomsRepository = procedureMonitorstatRequestNomsRepository;
        }

        [Route("")]
        public IList<EntityNomVO> GetProcedureMonitorstatRequests(int identifier, string discriminator = null, string term = null, int offset = 0, int? limit = null)
        {
            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                return this.procedureMonitorstatRequestNomsRepository.GetNSIDeclarationNomsForProcedure(identifier, term, offset, limit);
            }

            return this.procedureMonitorstatRequestNomsRepository.GetNomsForProcedure(identifier, term, offset, limit);
        }
    }
}
