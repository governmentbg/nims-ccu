namespace Eumis.Authentication.Authorization.ClaimsContexts.FIReimbursedAmount
{
    internal delegate IFIReimbursedAmountClaimsContext FIReimbursedAmountClaimsContextFactory(int fiReimbursedAmountId);

    internal interface IFIReimbursedAmountClaimsContext
    {
        int FIReimbursedAmountId { get; }

        int ProgrammeId { get; }
    }
}
