using Autofac.Features.AttributeFilters;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectDossier
{
    internal class ProjectDossierClaimsContext : ClaimsContext, IProjectDossierClaimsContext
    {
        private int projectId;

        private IClaimsCache claimsCache;
        private IProceduresRepository proceduresRepository;
        private IProjectsRepository projectsRepository;

        public ProjectDossierClaimsContext(
            int projectId,
            [KeyFilter(ClaimsCaches.ProjectDossier)]IClaimsCache claimsCache,
            IProceduresRepository proceduresRepository,
            IProjectsRepository projectsRepository)
            : base(claimsCache)
        {
            this.projectId = projectId;
            this.claimsCache = claimsCache;
            this.proceduresRepository = proceduresRepository;
            this.projectsRepository = projectsRepository;
        }

        public int ProjectId
        {
            get
            {
                return this.projectId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.projectId,
                    new ClaimKey("ProgrammeId"),
                    () => this.proceduresRepository.GetPrimaryProcedureProgrammeId(this.ProcedureId));
            }
        }

        public int ProcedureId
        {
            get
            {
                return this.GetClaim(
                    this.projectId,
                    new ClaimKey("ProcedureId"),
                    () => this.projectsRepository.GetProcedureId(this.projectId));
            }
        }
    }
}
