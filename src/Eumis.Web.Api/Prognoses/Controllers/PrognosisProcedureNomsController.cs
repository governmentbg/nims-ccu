using System.Collections.Generic;
using System.Web.Http;
using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.Web.Api.Prognoses.Controllers
{
    [RoutePrefix("api/nomenclatures/prognosisProcedures")]
    public class PrognosisProcedureNomsController : ApiController
    {
        private IProcedureNomsRepository procedureNomsRepository;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public PrognosisProcedureNomsController(
            IProcedureNomsRepository procedureNomsRepository,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.procedureNomsRepository = procedureNomsRepository;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.procedureNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            return this.procedureNomsRepository.GetProcedureNoms(programmeIds, term, offset, limit);
        }
    }
}
