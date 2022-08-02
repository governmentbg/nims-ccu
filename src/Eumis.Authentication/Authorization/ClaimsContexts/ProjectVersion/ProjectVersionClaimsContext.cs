using Autofac.Features.AttributeFilters;
using Eumis.Data.Projects.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectVersion
{
    internal class ProjectVersionClaimsContext : ClaimsContext, IProjectVersionClaimsContext
    {
        private int versionId;

        private IClaimsCache claimsCache;
        private IProjectVersionXmlsRepository projectVersionXmlsRepository;
        private IProjectsRepository projectsRepository;

        public ProjectVersionClaimsContext(
            int versionId,
            [KeyFilter(ClaimsCaches.ProjectVersion)]IClaimsCache claimsCache,
            IProjectVersionXmlsRepository projectVersionXmlsRepository,
            IProjectsRepository projectsRepository)
            : base(claimsCache)
        {
            this.versionId = versionId;
            this.claimsCache = claimsCache;
            this.projectVersionXmlsRepository = projectVersionXmlsRepository;
            this.projectsRepository = projectsRepository;
        }

        public int ProjectVersionId
        {
            get
            {
                return this.versionId;
            }
        }

        public int ProjectId
        {
            get
            {
                return this.GetClaim(
                    this.versionId,
                    new ClaimKey("ProjectId"),
                    () => this.projectVersionXmlsRepository.GetProjectId(this.versionId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.versionId,
                    new ClaimKey("ProgrammeId"),
                    () => this.projectsRepository.GetPrimaryProgrammeId(this.ProjectId));
            }
        }

        public bool IsProjectInFinishedEvalSession()
        {
            return this.GetClaim(
                this.versionId,
                new ClaimKey("IsProjectInFinishedEvalSession"),
                () => this.projectsRepository.IsProjectInFinishedEvalSession(this.ProjectId));
        }
    }
}
