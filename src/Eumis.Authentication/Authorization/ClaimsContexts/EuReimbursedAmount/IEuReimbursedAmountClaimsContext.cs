namespace Eumis.Authentication.Authorization.ClaimsContexts.EuReimbursedAmount
{
    internal delegate IEuReimbursedAmountClaimsContext EuReimbursedAmountClaimsContextFactory(int euReimbursedAmountId);

    internal interface IEuReimbursedAmountClaimsContext
    {
        int EuReimbursedAmountId { get; }

        int ProgrammeId { get; }
    }
}
