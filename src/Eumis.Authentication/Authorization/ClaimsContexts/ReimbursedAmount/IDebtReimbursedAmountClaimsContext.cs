namespace Eumis.Authentication.Authorization.ClaimsContexts.ReimbursedAmount
{
    internal delegate IDebtReimbursedAmountClaimsContext DebtReimbursedAmountClaimsContextFactory(int reimbursedAmountId);

    internal interface IDebtReimbursedAmountClaimsContext
    {
        int ReimbursedAmountId { get; }

        int ProgrammeId { get; }

        int ContractId { get; }
    }
}
