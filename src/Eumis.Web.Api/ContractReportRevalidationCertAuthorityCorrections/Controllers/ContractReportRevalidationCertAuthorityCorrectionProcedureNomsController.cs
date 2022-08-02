using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportRevalidationCertAuthorityCorrectionProcedures")]
    public class ContractReportRevalidationCertAuthorityCorrectionProcedureNomsController : ApiController
    {
        private IPermissionsRepository permissionsRepository;
        private IAccessContext accessContext;
        private IProcedureNomsRepository procedureNomsRepository;

        public ContractReportRevalidationCertAuthorityCorrectionProcedureNomsController(
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext,
            IProcedureNomsRepository procedureNomsRepository)
        {
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
            this.procedureNomsRepository = procedureNomsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.procedureNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(int programmeId, string term = null, int offset = 0, int? limit = null)
        {
            var programmeIds = System.Array.Empty<int>();

            if (!programmeIds.Contains(programmeId))
            {
                return new List<EntityNomVO>();
            }

            return this.procedureNomsRepository.GetProcedureNoms(programmeId, term, offset, limit);
        }
    }
}
