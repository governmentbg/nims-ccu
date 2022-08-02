namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertAuthorityFinancialCorrection
{
    internal delegate IContractReportCertAuthorityFinancialCorrectionClaimsContext ContractReportCertAuthorityFinancialCorrectionClaimsContextFactory(int contractReportCertAuthorityFinancialCorrectionId);

    internal interface IContractReportCertAuthorityFinancialCorrectionClaimsContext
    {
        int ContractReportCertAuthorityFinancialCorrectionId { get; }

        int ContractReportId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
