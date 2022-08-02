namespace Eumis.Authentication.Authorization.ClaimsContexts.AuditAuthorityCommunication
{
    internal delegate IAuditAuthorityCommunicationClaimsContext AuditAuthorityCommunicationClaimsContextFactory(int communicationId);

    internal interface IAuditAuthorityCommunicationClaimsContext
    {
        int ContractCommunicationId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
