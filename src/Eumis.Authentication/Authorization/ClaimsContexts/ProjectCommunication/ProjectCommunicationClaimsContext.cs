using Autofac.Features.AttributeFilters;
using Eumis.Data.Projects.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectCommunication
{
    internal class ProjectCommunicationClaimsContext : ClaimsContext, IProjectCommunicationClaimsContext
    {
        private int communicationId;

        private IClaimsCache claimsCache;
        private IProjectCommunicationsRepository projectCommunicationsRepository;
        private IProjectsRepository projectsRepository;

        public ProjectCommunicationClaimsContext(
            int communicationId,
            [KeyFilter(ClaimsCaches.ProjectCommunication)]IClaimsCache claimsCache,
            IProjectCommunicationsRepository projectCommunicationsRepository,
            IProjectsRepository projectsRepository)
            : base(claimsCache)
        {
            this.communicationId = communicationId;
            this.claimsCache = claimsCache;
            this.projectCommunicationsRepository = projectCommunicationsRepository;
            this.projectsRepository = projectsRepository;
        }

        public int ProjectCommunicationId
        {
            get
            {
                return this.communicationId;
            }
        }

        public int ProjectId
        {
            get
            {
                return this.GetClaim(
                    this.communicationId,
                    new ClaimKey("ProjectId"),
                    () => this.projectCommunicationsRepository.GetProjectId(this.communicationId));
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.communicationId,
                    new ClaimKey("ProgrammeId"),
                    () => this.projectsRepository.GetPrimaryProgrammeId(this.ProjectId));
            }
        }

        public bool IsProjectInFinishedEvalSession()
        {
            return this.GetClaim(
                this.communicationId,
                new ClaimKey("IsProjectInFinishedEvalSession"),
                () => this.projectsRepository.IsProjectInFinishedEvalSession(this.ProjectId));
        }
    }
}
