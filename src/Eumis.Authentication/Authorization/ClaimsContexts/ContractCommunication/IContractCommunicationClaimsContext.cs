namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractCommunication
{
    internal delegate IContractCommunicationClaimsContext ContractCommunicationClaimsContextFactory(int communicationId);

    internal interface IContractCommunicationClaimsContext
    {
        int ContractCommunicationId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
