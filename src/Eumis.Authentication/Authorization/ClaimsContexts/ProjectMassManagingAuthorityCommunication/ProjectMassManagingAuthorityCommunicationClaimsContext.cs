using Autofac.Features.AttributeFilters;
using Eumis.Data.Projects.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectMassManagingAuthorityCommunication
{
    internal class ProjectMassManagingAuthorityCommunicationClaimsContext : ClaimsContext, IProjectMassManagingAuthorityCommunicationClaimsContext
    {
        private int projectMassCommunicationId;

        private IClaimsCache claimsCache;
        private IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository;

        public ProjectMassManagingAuthorityCommunicationClaimsContext(
            int projectMassCommunicationId,
            [KeyFilter(ClaimsCaches.ProjectMassManagingAuthorityCommunication)]IClaimsCache claimsCache,
            IProjectMassManagingAuthorityCommunicationsRepository projectMassManagingAuthorityCommunicationsRepository)
            : base(claimsCache)
        {
            this.projectMassCommunicationId = projectMassCommunicationId;
            this.claimsCache = claimsCache;
            this.projectMassManagingAuthorityCommunicationsRepository = projectMassManagingAuthorityCommunicationsRepository;
        }

        public int ProjectMassCommunicationId
        {
            get
            {
                return this.projectMassCommunicationId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.projectMassCommunicationId,
                    new ClaimKey("ProgrammeId"),
                    () => this.projectMassManagingAuthorityCommunicationsRepository.GetPrimaryProcedureShareProgrammeId(this.projectMassCommunicationId));
            }
        }
    }
}
