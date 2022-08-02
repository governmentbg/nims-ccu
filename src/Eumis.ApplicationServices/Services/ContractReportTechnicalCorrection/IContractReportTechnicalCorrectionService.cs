using System;
using System.Collections.Generic;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;

namespace Eumis.ApplicationServices.Services.ContractReportTechnicalCorrection
{
    public interface IContractReportTechnicalCorrectionService
    {
        // ContractReportTechnicalCorrection
        IList<string> CanCreateContractReportTechnicalCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportTechnicalCorrection CreateContractReportTechnicalCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportTechnicalCorrection UpdateContractReportTechnicalCorrection(
            int contractReportTechnicalCorrectionId,
            byte[] version,
            DateTime? correctionDate,
            Guid? blobKey,
            string notes);

        IList<string> CanDeleteContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId);

        Eumis.Domain.Contracts.ContractReportTechnicalCorrection DeleteContractReportTechnicalCorrection(int contractReportTechnicalCorrectionId, byte[] version);

        IList<string> CanChangeContractReportTechnicalCorrectionStatusToEnded(int contractReportTechnicalCorrectionId);

        Eumis.Domain.Contracts.ContractReportTechnicalCorrection ChangeContractReportTechnicalCorrectionStatus(int contractReportTechnicalCorrectionId, byte[] version, ContractReportTechnicalCorrectionStatus status);

        // ContractReportTechnicalCorrectionIndicator
        ContractReportTechnicalCorrectionIndicator CreateContractReportTechnicalCorrectionIndicator(
            int contractReportTechnicalCorrectionId,
            int contractReportIndicatorId);

        ContractReportTechnicalCorrectionIndicator UpdateContractReportTechnicalCorrectionIndicator(
            int contractReportTechnicalCorrectionIndicatorId,
            byte[] version,
            string notes,
            decimal? correctedApprovedPeriodAmountMen,
            decimal? correctedApprovedPeriodAmountWomen,
            decimal correctedApprovedPeriodAmountTotal,
            decimal? correctedApprovedCumulativeAmountMen,
            decimal? correctedApprovedCumulativeAmountWomen,
            decimal correctedApprovedCumulativeAmountTotal,
            decimal? correctedApprovedResidueAmountMen,
            decimal? correctedApprovedResidueAmountWomen,
            decimal correctedApprovedResidueAmountTotal);

        ContractReportTechnicalCorrectionIndicator DeleteContractReportTechnicalCorrectionIndicator(int contractReportTechnicalCorrectionIndicatorId, byte[] version);

        IList<string> CanChangeContractReportTechnicalCorrectionIndicatorStatusToEnded(int contractReportTechnicalCorrectionIndicatorId);

        ContractReportTechnicalCorrectionIndicator ChangeContractReportTechnicalCorrectionIndicatorStatus(int contractReportTechnicalCorrectionIndicatorId, byte[] version, ContractReportTechnicalCorrectionIndicatorStatus status);
    }
}
