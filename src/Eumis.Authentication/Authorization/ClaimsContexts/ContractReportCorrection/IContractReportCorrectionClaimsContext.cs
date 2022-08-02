namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportCorrection
{
    internal delegate IContractReportCorrectionClaimsContext ContractReportCorrectionClaimsContextFactory(int contractReportCorrectionId);

    internal interface IContractReportCorrectionClaimsContext
    {
        int ContractReportCorrectionId { get; }

        int ProgrammeId { get; }

        int? ContractId { get; }
    }
}
