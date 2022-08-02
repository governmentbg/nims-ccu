namespace Eumis.Authentication.Authorization.ClaimsContexts.ReimbursedAmount
{
    internal delegate IContractReimbursedAmountClaimsContext ContractReimbursedAmountClaimsContextFactory(int reimbursedAmountId);

    internal interface IContractReimbursedAmountClaimsContext
    {
        int ReimbursedAmountId { get; }

        int ProgrammeId { get; }

        int ContractId { get; }
    }
}
