using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportCertAuthorityFinancialCorrectionService
{
    public interface IContractReportCertAuthorityFinancialCorrectionService
    {
        // ContractReportCertAuthorityFinancialCorrection
        IList<string> CanCreateContractReportCertAuthorityFinancialCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrection CreateContractReportCertAuthorityFinancialCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrection UpdateContractReportCertAuthorityFinancialCorrection(
            int contractReportCertAuthorityFinancialCorrectionId,
            byte[] version,
            DateTime? certCorrectionDate,
            Guid? blobKey,
            string notes);

        IList<string> CanDeleteContractReportCertAuthorityFinancialCorrection(int contractReportCertAuthorityFinancialCorrectionId);

        Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrection DeleteContractReportCertAuthorityFinancialCorrection(int contractReportCertAuthorityFinancialCorrectionId, byte[] version);

        IList<string> CanChangeContractReportCertAuthorityFinancialCorrectionStatusToEnded(int contractReportCertAuthorityFinancialCorrectionId);

        IList<string> CanChangeContractReportCertAuthorityFinancialCorrectionStatusToDraft(int contractReportCertAuthorityFinancialCorrectionId);

        Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrection ChangeContractReportCertAuthorityFinancialCorrectionStatus(int contractReportCertAuthorityFinancialCorrectionId, byte[] version, ContractReportCertAuthorityFinancialCorrectionStatus status);

        // ContractReportCertAuthorityFinancialCorrectionCSD
        Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionCSD CreateContractReportCertAuthorityFinancialCorrectionCSD(
            int contractReportCertAuthorityFinancialCorrectionId,
            int contractReportFinancialCSDBudgetItemId);

        Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionCSD UpdateContractReportCertAuthorityFinancialCorrectionCSD(
            int contractReportCertAuthorityFinancialCorrectionId,
            byte[] version,
            Sign? sign,
            string notes,
            decimal? certifiedEuAmount,
            decimal? certifiedBgAmount,
            decimal? certifiedBfpTotalAmount,
            decimal? certifiedSelfAmount,
            decimal? certifiedTotalAmount);

        Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionCSD DeleteContractReportCertAuthorityFinancialCorrectionCSD(int contractReportCertAuthorityFinancialCorrectionCSDId, byte[] version);

        IList<string> CanChangeContractReportCertAuthorityFinancialCorrectionCSDStatusToEnded(int contractReportCertAuthorityFinancialCorrectionCSDId);

        Eumis.Domain.Contracts.ContractReportCertAuthorityFinancialCorrectionCSD ChangeContractReportCertAuthorityFinancialCorrectionCSDStatus(int contractReportCertAuthorityFinancialCorrectionCSDId, byte[] version, ContractReportCertAuthorityFinancialCorrectionCSDStatus status);
    }
}
