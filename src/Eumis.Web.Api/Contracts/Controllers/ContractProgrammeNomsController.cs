using System.Collections.Generic;
using System.Web.Http;
using Eumis.Common.Auth;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractProgrammes")]
    public class ContractProgrammeNomsController : EntityNomsController<Programme, EntityNomVO>
    {
        private IProgrammeNomsRepository programmeNomsRepository;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public ContractProgrammeNomsController(
            IProgrammeNomsRepository programmeNomsRepository,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
            : base(programmeNomsRepository)
        {
            this.programmeNomsRepository = programmeNomsRepository;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetContractProgrammeNoms(int procedureId, string term = null, int offset = 0, int? limit = null)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanWrite);

            return this.programmeNomsRepository.GetProgrammeNoms(term, offset, limit, procedureId: procedureId, programmeIds: programmeIds);
        }
    }
}
