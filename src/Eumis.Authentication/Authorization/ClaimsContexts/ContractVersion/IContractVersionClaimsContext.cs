namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractVersion
{
    internal delegate IContractVersionClaimsContext ContractVersionClaimsContextFactory(int versionId);

    internal interface IContractVersionClaimsContext
    {
        int ContractVersionId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
