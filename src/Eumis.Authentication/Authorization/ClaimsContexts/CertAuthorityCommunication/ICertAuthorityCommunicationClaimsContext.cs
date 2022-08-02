namespace Eumis.Authentication.Authorization.ClaimsContexts.CertAuthorityCommunication
{
    internal delegate ICertAuthorityCommunicationClaimsContext CertAuthorityCommunicationClaimsContextFactory(int communicationId);

    internal interface ICertAuthorityCommunicationClaimsContext
    {
        int ContractCommunicationId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
