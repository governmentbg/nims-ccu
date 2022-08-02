namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialRevalidation
{
    internal delegate IContractReportFinancialRevalidationClaimsContext ContractReportFinancialRevalidationClaimsContextFactory(int contractReportRevalidationId);

    internal interface IContractReportFinancialRevalidationClaimsContext
    {
        int ContractReportRevalidationId { get; }

        int ContractReportId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
