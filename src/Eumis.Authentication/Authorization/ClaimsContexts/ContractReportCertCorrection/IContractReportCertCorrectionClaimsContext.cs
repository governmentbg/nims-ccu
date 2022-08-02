namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertCorrection
{
    internal delegate IContractReportCertCorrectionClaimsContext ContractReportCertCorrectionClaimsContextFactory(int contractReportCertCorrectionId);

    internal interface IContractReportCertCorrectionClaimsContext
    {
        int ContractReportCertCorrectionId { get; }

        int ProgrammeId { get; }
    }
}
