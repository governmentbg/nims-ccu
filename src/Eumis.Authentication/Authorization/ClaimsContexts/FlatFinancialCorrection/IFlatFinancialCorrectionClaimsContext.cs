namespace Eumis.Authentication.Authorization.ClaimsContexts.FinancialCorrection
{
    internal delegate IFlatFinancialCorrectionClaimsContext FlatFinancialCorrectionClaimsContextFactory(int flatFinancialCorrectionId);

    internal interface IFlatFinancialCorrectionClaimsContext
    {
        int FlatFinancialCorrectionId { get; }

        int ProgrammeId { get; }
    }
}
