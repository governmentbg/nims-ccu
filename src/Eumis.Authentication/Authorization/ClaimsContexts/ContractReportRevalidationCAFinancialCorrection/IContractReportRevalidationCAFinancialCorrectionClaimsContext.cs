namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidationCAFinancialCorrection
{
    internal delegate IContractReportRevalidationCAFinancialCorrectionClaimsContext ContractReportRevalidationCAFinancialCorrectionClaimsContextFactory(int contractReportRevalidationCertAuthorityFinancialCorrectionId);

    internal interface IContractReportRevalidationCAFinancialCorrectionClaimsContext
    {
        int ContractReportRevalidationCertAuthorityFinancialCorrectionId { get; }

        int ContractReportId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
