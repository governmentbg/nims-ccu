using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.ContractReportRevalidationCertAuthorityCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/contractReportRevalidationCertAuthorityCorrectionProgrammes")]
    public class ContractReportRevalidationCertAuthorityCorrectionProgrammeNomsController : ApiController
    {
        private IProgrammeNomsRepository programmeNomsRepository;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public ContractReportRevalidationCertAuthorityCorrectionProgrammeNomsController(
            IProgrammeNomsRepository programmeNomsRepository,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.programmeNomsRepository = programmeNomsRepository;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.programmeNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null, bool readOnly = true)
        {
            var permission = readOnly ? ContractReportPermissions.CanCheck : ContractReportPermissions.CanWrite;

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, permission);

            return this.programmeNomsRepository.GetProgrammeNoms(term, offset, limit, procedureId: null, programmeIds: programmeIds);
        }
    }
}
