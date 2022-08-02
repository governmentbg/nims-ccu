namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCheck
{
    internal delegate IContractReportCheckClaimsContext ContractReportCheckClaimsContextFactory(int contractReportId);

    internal interface IContractReportCheckClaimsContext
    {
        int ContractReportId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
