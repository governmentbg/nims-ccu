using System.Collections.Generic;
using System.Web.Http;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.OperationalMap.ProgrammePriorities.Controllers
{
    [RoutePrefix("api/nomenclatures/programmePriorityCompanies")]
    public class ProgrammePriorityCompanyNomsController : EntityNomsController<ProgrammePriorityCompany, EntityNomVO>
    {
        private IProgrammePriorityCompanyNomsRepository programmePriorityCompanyNomsRepository;

        public ProgrammePriorityCompanyNomsController(IProgrammePriorityCompanyNomsRepository programmePriorityCompanyNomsRepository)
            : base(programmePriorityCompanyNomsRepository)
        {
            this.programmePriorityCompanyNomsRepository = programmePriorityCompanyNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetProgrammePriorityCompanyNoms(ProgrammePriorityType programmePriorityType, string term = null, bool higherOrderCompany = false, int offset = 0, int? limit = null)
        {
            return this.programmePriorityCompanyNomsRepository.GetProgrammePriorityCompanyNoms(programmePriorityType, term, higherOrderCompany, offset, limit);
        }
    }
}
