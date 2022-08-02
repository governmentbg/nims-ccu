namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractReportTechnicalCorrection
{
    internal delegate IContractReportTechnicalCorrectionClaimsContext ContractReportTechnicalCorrectionClaimsContextFactory(int contractReportTechnicalCorrectionId);

    internal interface IContractReportTechnicalCorrectionClaimsContext
    {
        int ContractReportTechnicalCorrectionId { get; }

        int ContractReportId { get; }

        int ContractId { get; }

        int ProgrammeId { get; }
    }
}
