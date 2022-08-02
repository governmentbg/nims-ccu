using Autofac.Features.AttributeFilters;
using Eumis.Data.Projects.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Project
{
    internal class ProjectClaimsContext : ClaimsContext, IProjectClaimsContext
    {
        private int projectId;

        private IClaimsCache claimsCache;
        private IProjectsRepository projectsRepository;

        public ProjectClaimsContext(
            int projectId,
            [KeyFilter(ClaimsCaches.Project)]IClaimsCache claimsCache,
            IProjectsRepository projectsRepository)
            : base(claimsCache)
        {
            this.projectId = projectId;
            this.claimsCache = claimsCache;
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
                    () => this.projectsRepository.GetPrimaryProgrammeId(this.projectId));
            }
        }

        public bool IsProjectInFinishedEvalSession()
        {
            return this.GetClaim(
                this.projectId,
                new ClaimKey("IsProjectInFinishedEvalSession"),
                () => this.projectsRepository.IsProjectInFinishedEvalSession(this.projectId));
        }
    }
}
