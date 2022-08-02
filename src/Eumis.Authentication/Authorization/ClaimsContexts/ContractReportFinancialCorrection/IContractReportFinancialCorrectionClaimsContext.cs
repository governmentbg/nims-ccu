namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportFinancialCorrection
{
    internal delegate IContractReportFinancialCorrectionClaimsContext ContractReportFinancialCorrectionClaimsContextFactory(int contractReportCorrectionId);

    internal interface IContractReportFinancialCorrectionClaimsContext
    {
        int ContractReportCorrectionId { get; }

        int ContractReportId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
