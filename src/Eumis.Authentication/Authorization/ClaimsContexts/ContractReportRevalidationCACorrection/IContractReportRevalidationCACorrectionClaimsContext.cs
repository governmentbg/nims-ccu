namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidationCACorrection
{
    internal delegate IContractReportRevalidationCACorrectionClaimsContext ContractReportRevalidationCACorrectionClaimsContextFactory(int contractReportRevalidationCertAuthorityCorrectionId);

    internal interface IContractReportRevalidationCACorrectionClaimsContext
    {
        int ContractReportRevalidationCertAuthorityCorrectionId { get; }

        int ProgrammeId { get; }
    }
}
