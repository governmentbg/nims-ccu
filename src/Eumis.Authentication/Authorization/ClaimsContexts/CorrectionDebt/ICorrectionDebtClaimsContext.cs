namespace Eumis.Authentication.Authorization.ClaimsContexts.CorrectionDebt
{
    internal delegate ICorrectionDebtClaimsContext CorrectionDebtClaimsContextFactory(int correctionDebtId);

    internal interface ICorrectionDebtClaimsContext
    {
        int CorrectionDebtId { get; }

        int FlatFinancialCorrectionId { get; }

        int ProgrammeId { get; }
    }
}
