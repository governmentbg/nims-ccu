using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/nomenclatures/programmes")]
    public class ProgrammeNomsController : EntityNomsController<Programme, EntityNomVO>
    {
        private IProgrammeNomsRepository programmeNomsRepository;

        public ProgrammeNomsController(IProgrammeNomsRepository programmeNomsRepository)
            : base(programmeNomsRepository)
        {
            this.programmeNomsRepository = programmeNomsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetProcedureProgrammeNoms(int procedureId, string term = null, int offset = 0, int? limit = null)
        {
            return this.programmeNomsRepository.GetProgrammeNoms(term, offset, limit, procedureId: procedureId);
        }
    }
}
