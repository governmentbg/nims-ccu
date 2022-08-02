using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportFinancialCertCorrection
{
    public interface IContractReportFinancialCertCorrectionService
    {
        // ContractReportFinancialCertCorrection
        IList<string> CanCreateContractReportFinancialCertCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportFinancialCertCorrection CreateContractReportFinancialCertCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportFinancialCertCorrection UpdateContractReportFinancialCertCorrection(
            int contractReportFinancialCertCorrectionId,
            byte[] version,
            DateTime? certCorrectionDate,
            Guid? blobKey,
            string notes);

        IList<string> CanDeleteContractReportFinancialCertCorrection(int contractReportFinancialCertCorrectionId);

        Eumis.Domain.Contracts.ContractReportFinancialCertCorrection DeleteContractReportFinancialCertCorrection(int contractReportFinancialCertCorrectionId, byte[] version);

        IList<string> CanChangeContractReportFinancialCertCorrectionStatusToEnded(int contractReportFinancialCertCorrectionId);

        IList<string> CanChangeContractReportFinancialCertCorrectionStatusToDraft(int contractReportFinancialCertCorrectionId);

        Eumis.Domain.Contracts.ContractReportFinancialCertCorrection ChangeContractReportFinancialCertCorrectionStatus(int contractReportFinancialCertCorrectionId, byte[] version, ContractReportFinancialCertCorrectionStatus status);

        // ContractReportFinancialCertCorrectionCSD
        Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD CreateContractReportFinancialCertCorrectionCSD(
            int contractReportFinancialCertCorrectionId,
            int contractReportFinancialCSDBudgetItemId);

        Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD UpdateContractReportFinancialCertCorrectionCSD(
            int contractReportFinancialCertCorrectionCSDId,
            byte[] version,
            Sign? sign,
            string notes,
            decimal? certifiedEuAmount,
            decimal? certifiedBgAmount,
            decimal? certifiedBfpTotalAmount,
            decimal? certifiedSelfAmount,
            decimal? certifiedTotalAmount);

        Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD DeleteContractReportFinancialCertCorrectionCSD(int contractReportFinancialCertCorrectionCSDId, byte[] version);

        IList<string> CanChangeContractReportFinancialCertCorrectionCSDStatusToEnded(int contractReportFinancialCertCorrectionCSDId);

        Eumis.Domain.Contracts.ContractReportFinancialCertCorrectionCSD ChangeContractReportFinancialCertCorrectionCSDStatus(int contractReportFinancialCertCorrectionCSDId, byte[] version, ContractReportFinancialCertCorrectionCSDStatus status);
    }
}
