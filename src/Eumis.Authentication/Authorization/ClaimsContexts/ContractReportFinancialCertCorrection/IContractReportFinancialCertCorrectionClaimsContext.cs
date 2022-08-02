namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialCertCorrection
{
    internal delegate IContractReportFinancialCertCorrectionClaimsContext ContractReportFinancialCertCorrectionClaimsContextFactory(int contractReportCertCorrectionId);

    internal interface IContractReportFinancialCertCorrectionClaimsContext
    {
        int ContractReportCertCorrectionId { get; }

        int ContractReportId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
