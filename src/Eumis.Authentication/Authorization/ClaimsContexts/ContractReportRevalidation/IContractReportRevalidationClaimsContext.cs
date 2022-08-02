namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportRevalidation
{
    internal delegate IContractReportRevalidationClaimsContext ContractReportRevalidationClaimsContextFactory(int contractReportRevalidationId);

    internal interface IContractReportRevalidationClaimsContext
    {
        int ContractReportRevalidationId { get; }

        int ProgrammeId { get; }
    }
}
