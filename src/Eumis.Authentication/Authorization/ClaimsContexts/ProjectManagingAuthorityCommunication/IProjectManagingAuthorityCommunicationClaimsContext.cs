namespace Eumis.Authentication.Authorization.ClaimsContexts.ProjectManagingAuthorityCommunication
{
    internal delegate IProjectManagingAuthorityCommunicationClaimsContext ProjectManagingAuthorityCommunicationClaimsContextFactory(int communicationId);

    internal interface IProjectManagingAuthorityCommunicationClaimsContext
    {
        int ProjectCommunicationId { get; }

        int ProgrammeId { get; }
    }
}
