namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCertAuthorityCorrection
{
    internal delegate IContractReportCertAuthorityCorrectionClaimsContext ContractReportCertAuthorityCorrectionClaimsContextFactory(int contractReportCertAuthorityCorrectionId);

    internal interface IContractReportCertAuthorityCorrectionClaimsContext
    {
        int ContractReportCertAuthorityCorrectionId { get; }

        int ProgrammeId { get; }
    }
}
