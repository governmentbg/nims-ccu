using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportFinancialRevalidation
{
    public interface IContractReportFinancialRevalidationService
    {
        // ContractReportFinancialRevalidation
        IList<string> CanCreateContractReportFinancialRevalidation(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportFinancialRevalidation CreateContractReportFinancialRevalidation(string contractNum, string contractReportNum);

        Eumis.Domain.Contracts.ContractReportFinancialRevalidation UpdateContractReportFinancialRevalidation(
            int contractReportFinancialRevalidationId,
            byte[] version,
            DateTime? revalidationDate,
            Guid? blobKey,
            string notes);

        IList<string> CanDeleteContractReportFinancialRevalidation(int contractReportFinancialRevalidationId);

        Eumis.Domain.Contracts.ContractReportFinancialRevalidation DeleteContractReportFinancialRevalidation(int contractReportFinancialRevalidationId, byte[] version);

        IList<string> CanChangeContractReportFinancialRevalidationStatusToEnded(int contractReportFinancialRevalidationId);

        IList<string> CanChangeContractReportFinancialRevalidationStatusToDraft(int contractReportFinancialRevalidationId);

        Eumis.Domain.Contracts.ContractReportFinancialRevalidation ChangeContractReportFinancialRevalidationStatus(int contractReportFinancialRevalidationId, byte[] version, ContractReportFinancialRevalidationStatus status);

        // ContractReportFinancialRevalidationCSD
        Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD CreateContractReportFinancialRevalidationCSD(
            int contractReportFinancialRevalidationId,
            int contractReportFinancialCSDBudgetItemId);

        Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD UpdateContractReportFinancialRevalidationCSD(
            int contractReportFinancialRevalidationCSDId,
            byte[] version,
            string notes,
            decimal? revalidatedEuAmount,
            decimal? revalidatedBgAmount,
            decimal? revalidatedBfpTotalAmount,
            decimal? revalidatedSelfAmount,
            decimal? revalidatedTotalAmount);

        Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD DeleteContractReportFinancialRevalidationCSD(int contractReportFinancialRevalidationCSDId, byte[] version);

        IList<string> CanChangeContractReportFinancialRevalidationCSDStatusToEnded(int contractReportFinancialRevalidationCSDId);

        Eumis.Domain.Contracts.ContractReportFinancialRevalidationCSD ChangeContractReportFinancialRevalidationCSDStatus(int contractReportFinancialRevalidationCSDId, byte[] version, ContractReportFinancialRevalidationCSDStatus status);
    }
}
