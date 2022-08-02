namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectMassManagingAuthorityCommunication
{
    internal delegate IProjectMassManagingAuthorityCommunicationClaimsContext ProjectMassManagingAuthorityCommunicationClaimsContextFactory(int projectMassCommunicationId);

    internal interface IProjectMassManagingAuthorityCommunicationClaimsContext
    {
        int ProjectMassCommunicationId { get; }

        int ProgrammeId { get; }
    }
}
