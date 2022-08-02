namespace Eumis.Authentication.Authorization.ClaimsContexts.ActuallyPaidAmount
{
    internal delegate IActuallyPaidAmountClaimsContext ActuallyPaidAmountClaimsContextFactory(int actuallyPaidAmountId);

    internal interface IActuallyPaidAmountClaimsContext
    {
        int ActuallyPaidAmountId { get; }

        int ProgrammeId { get; }

        int ContractId { get; }
    }
}
