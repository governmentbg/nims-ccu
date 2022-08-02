using Eumis.Common.Auth;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Contracts.ViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.FinancialCorrections.Controllers
{
    [RoutePrefix("api/nomenclatures/projectDossierContracts")]
    public class ProjectDossierContractNomsController : ApiController
    {
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IProjectDossierContractNomsRepository projectDossierContractNomsRepository;

        public ProjectDossierContractNomsController(
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IProjectDossierContractNomsRepository projectDossierContractNomsRepository)
        {
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.projectDossierContractNomsRepository = projectDossierContractNomsRepository;
        }

        [Route("{id:int}")]
        public ProjectDossierContractNomVO GetNom(int id)
        {
            return this.projectDossierContractNomsRepository.GetNom(id);
        }

        [Route("")]
        public IEnumerable<ProjectDossierContractNomVO> GetNoms(string projectNumber = null, string term = null, int offset = 0, int? limit = null)
        {
            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, ProjectDossierPermissions.CanRead);

            return this.projectDossierContractNomsRepository.GetNoms(projectNumber, term, offset, limit, programmeIds);
        }
    }
}
