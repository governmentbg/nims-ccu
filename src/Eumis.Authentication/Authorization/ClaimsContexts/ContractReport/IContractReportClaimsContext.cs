namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReport
{
    internal delegate IContractReportClaimsContext ContractReportClaimsContextFactory(int contractReportId);

    internal interface IContractReportClaimsContext
    {
        int ContractReportId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
