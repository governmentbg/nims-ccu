using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportFinancialCorrection
{
    public interface IContractReportFinancialCorrectionService
    {
        // ContractReportFinancialCorrection
        IList<string> CanCreateContractReportFinancialCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportFinancialCorrection CreateContractReportFinancialCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportFinancialCorrection UpdateContractReportFinancialCorrection(
            int contractReportFinancialCorrectionId,
            byte[] version,
            DateTime? correctionDate,
            Guid? blobKey,
            string notes);

        IList<string> CanDeleteContractReportFinancialCorrection(int contractReportFinancialCorrectionId);

        Eumis.Domain.Contracts.ContractReportFinancialCorrection DeleteContractReportFinancialCorrection(int contractReportFinancialCorrectionId, byte[] version);

        IList<string> CanChangeContractReportFinancialCorrectionStatusToEnded(int contractReportFinancialCorrectionId);

        IList<string> CanChangeContractReportFinancialCorrectionStatusToDraft(int contractReportFinancialCorrectionId);

        Eumis.Domain.Contracts.ContractReportFinancialCorrection ChangeContractReportFinancialCorrectionStatus(int contractReportFinancialCorrectionId, byte[] version, ContractReportFinancialCorrectionStatus status);

        // ContractReportFinancialCorrectionCSD
        Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD CreateContractReportFinancialCorrectionCSD(
            int contractReportFinancialCorrectionId,
            int contractReportFinancialCSDBudgetItemId);

        Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD UpdateContractReportFinancialCorrectionCSD(
            int contractReportFinancialCorrectionCSDId,
            byte[] version,
            Sign? sign,
            string notes,
            decimal? correctedUnapprovedEuAmount,
            decimal? correctedUnapprovedBgAmount,
            decimal? correctedUnapprovedBfpTotalAmount,
            decimal? correctedUnapprovedSelfAmount,
            decimal? correctedUnapprovedTotalAmount,
            decimal? correctedUnapprovedByCorrectionEuAmount,
            decimal? correctedUnapprovedByCorrectionBgAmount,
            decimal? correctedUnapprovedByCorrectionBfpTotalAmount,
            decimal? correctedUnapprovedByCorrectionSelfAmount,
            decimal? correctedUnapprovedByCorrectionTotalAmount,
            decimal? correctedApprovedEuAmount,
            decimal? correctedApprovedBgAmount,
            decimal? correctedApprovedBfpTotalAmount,
            decimal? correctedApprovedSelfAmount,
            decimal? correctedApprovedTotalAmount,
            CorrectionType? correctionType,
            int? financialCorrectionId,
            int? irregularityId);

        Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD DeleteContractReportFinancialCorrectionCSD(int contractReportFinancialCorrectionCSDId, byte[] version);

        IList<string> CanChangeContractReportFinancialCorrectionCSDStatusToEnded(int contractReportFinancialCorrectionCSDId);

        Eumis.Domain.Contracts.ContractReportFinancialCorrectionCSD ChangeContractReportFinancialCorrectionCSDStatus(int contractReportFinancialCorrectionCSDId, byte[] version, ContractReportFinancialCorrectionCSDStatus status);
    }
}
