namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractDebt
{
    internal delegate IContractDebtClaimsContext ContractDebtClaimsContextFactory(int contractDebtId);

    internal interface IContractDebtClaimsContext
    {
        int ContractDebtId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
