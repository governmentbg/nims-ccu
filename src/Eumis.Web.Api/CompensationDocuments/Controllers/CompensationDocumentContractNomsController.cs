using System.Collections.Generic;
using System.Web.Http;
using Eumis.Common.Auth;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.Web.Api.CompensationDocuments.Controllers
{
    [RoutePrefix("api/nomenclatures/compensationDocumentContracts")]
    public class CompensationDocumentContractNomsController : ApiController
    {
        private IContractNomsRepository contractNomsRepository;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;

        public CompensationDocumentContractNomsController(
            IContractNomsRepository contractNomsRepository,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository)
        {
            this.contractNomsRepository = contractNomsRepository;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
        }

        [Route("{id:int}")]
        public EntityNomVO GetNom(int id)
        {
            return this.contractNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<EntityNomVO> GetNoms(string term = null, int offset = 0, int? limit = null)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            return this.contractNomsRepository.GetContracts(term, offset, limit, programmeIds);
        }
    }
}
