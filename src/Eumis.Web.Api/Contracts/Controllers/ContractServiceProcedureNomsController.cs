using System.Collections.Generic;
using System.Web.Http;
using Eumis.Common.Auth;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/nomenclatures/contractServiceProcedures")]
    public class ContractServiceProcedureNomsController : ApiController
    {
        private IProcedureNomsRepository proceduresNomRespository;
        private IPermissionsRepository permissionsRepository;
        private IAccessContext accessContext;

        public ContractServiceProcedureNomsController(
            IProcedureNomsRepository proceduresNomRespository,
            IPermissionsRepository permissionsRepository,
            IAccessContext accessContext)
        {
            this.proceduresNomRespository = proceduresNomRespository;
            this.permissionsRepository = permissionsRepository;
            this.accessContext = accessContext;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.proceduresNomRespository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ContractPermissions.CanWrite);

            return this.proceduresNomRespository.GetActiveProcedureNoms(programmeIds, term, offset, limit);
        }
    }
}
