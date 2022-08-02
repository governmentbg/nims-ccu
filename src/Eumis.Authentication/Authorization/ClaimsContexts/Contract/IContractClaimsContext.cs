namespace Eumis.Authentication.Authorization.ClaimsContexts.Contract
{
    internal delegate IContractClaimsContext ContractClaimsContextFactory(int contractId);

    internal interface IContractClaimsContext
    {
        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
