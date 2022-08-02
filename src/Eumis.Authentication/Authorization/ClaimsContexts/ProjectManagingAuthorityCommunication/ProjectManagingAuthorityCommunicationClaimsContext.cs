using Autofac.Features.AttributeFilters;
using Eumis.Data.Projects.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectManagingAuthorityCommunication
{
    internal class ProjectManagingAuthorityCommunicationClaimsContext : ClaimsContext, IProjectManagingAuthorityCommunicationClaimsContext
    {
        private int communicationId;

        private IClaimsCache claimsCache;
        private IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository;

        public ProjectManagingAuthorityCommunicationClaimsContext(
            int communicationId,
            [KeyFilter(ClaimsCaches.ProjectManagingAuthorityCommunication)]IClaimsCache claimsCache,
            IProjectManagingAuthorityCommunicationsRepository projectManagingAuthorityCommunicationsRepository)
            : base(claimsCache)
        {
            this.communicationId = communicationId;
            this.claimsCache = claimsCache;
            this.projectManagingAuthorityCommunicationsRepository = projectManagingAuthorityCommunicationsRepository;
        }

        public int ProjectCommunicationId
        {
            get
            {
                return this.communicationId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.communicationId,
                    new ClaimKey("ProgrammeId"),
                    () => this.projectManagingAuthorityCommunicationsRepository.GetProjectPrimaryProgrammeId(this.communicationId));
            }
        }
    }
}
