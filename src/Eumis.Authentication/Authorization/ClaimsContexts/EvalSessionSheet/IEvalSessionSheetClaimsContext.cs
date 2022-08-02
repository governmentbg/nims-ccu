namespace Eumis.Authentication.Authorization.ClaimsContexts.EvalSessionSheet
{
    internal delegate IEvalSessionSheetClaimsContext EvalSessionSheetClaimsContextFactory(int evalSessionSheetId);

    internal interface IEvalSessionSheetClaimsContext
    {
        int EvalSessionSheetId { get; }
    }
}
