namespace Eumis.Authentication.Authorization.ClaimsContexts.FinancialCorrection
{
    internal delegate IFinancialCorrectionClaimsContext FinancialCorrectionClaimsContextFactory(int financialCorrectionId);

    internal interface IFinancialCorrectionClaimsContext
    {
        int FinancialCorrectionId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
