using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportRevalidationCertAuthorityFinancialCorrection
{
    public interface IContractReportRevalidationCertAuthorityFinancialCorrectionService
    {
        // ContractReportRevalidationCertAuthorityFinancialCorrection
        IList<string> CanCreateContractReportRevalidationCertAuthorityFinancialCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrection CreateContractReportRevalidationCertAuthorityFinancialCorrection(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrection UpdateContractReportRevalidationCertAuthorityFinancialCorrection(
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            byte[] version,
            DateTime? certCorrectionDate,
            Guid? blobKey,
            string notes);

        IList<string> CanDeleteContractReportRevalidationCertAuthorityFinancialCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId);

        void DeleteContractReportRevalidationCertAuthorityFinancialCorrection(int contractReportRevalidationCertAuthorityFinancialCorrectionId, byte[] version);

        IList<string> CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatusToEnded(int contractReportRevalidationCertAuthorityFinancialCorrectionId);

        IList<string> CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatusToDraft(int contractReportRevalidationCertAuthorityFinancialCorrectionId);

        Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrection ChangeContractReportRevalidationCertAuthorityFinancialCorrectionStatus(int contractReportRevalidationCertAuthorityFinancialCorrectionId, byte[] version, ContractReportRevalidationCertAuthorityFinancialCorrectionStatus status);

        // ContractReportRevalidationCertAuthorityFinancialCorrectionCSD
        Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrectionCSD CreateContractReportRevalidationCertAuthorityFinancialCorrectionCSD(
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            int contractReportFinancialCSDBudgetItemId);

        Eumis.Domain.Contracts.ContractReportRevalidationCertAuthorityFinancialCorrectionCSD UpdateContractReportRevalidationCertAuthorityFinancialCorrectionCSD(
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            byte[] version,
            Sign? sign,
            string notes,
            decimal? revalidatedEuAmount,
            decimal? revalidatedBgAmount,
            decimal? revalidatedBfpTotalAmount,
            decimal? revalidatedSelfAmount,
            decimal? revalidatedTotalAmount);

        void DeleteContractReportRevalidationCertAuthorityFinancialCorrectionCSD(int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, byte[] version);

        IList<string> CanChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatusToEnded(int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId);

        ContractReportRevalidationCertAuthorityFinancialCorrectionCSD ChangeContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus(int contractReportRevalidationCertAuthorityFinancialCorrectionCSDId, byte[] version, ContractReportRevalidationCertAuthorityFinancialCorrectionCSDStatus status);
    }
}
