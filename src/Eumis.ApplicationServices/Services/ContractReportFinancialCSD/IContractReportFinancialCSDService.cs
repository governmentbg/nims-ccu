using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ContractReportFinancialCSD
{
    public interface IContractReportFinancialCSDService
    {
        void CreateContractReportFinancialCSDAndBudgetItems(ContractReportFinancial finance);

        void DeleteContractReportFinancialCSDAndBudgetItemsInDraft(ContractReportFinancial finance);

        ContractReportFinancialCSDBudgetItem UpdateContractReportFinancialCSDBudgetItem(
            int contractReportFinancialCSDBudgetItemId,
            byte[] version,
            bool? costSupportingDocumentApproved,
            string notes,
            decimal euAmount,
            decimal bgAmount,
            decimal? unapprovedEuAmount,
            decimal? unapprovedBgAmount,
            decimal? unapprovedBfpTotalAmount,
            decimal? unapprovedSelfAmount,
            decimal? unapprovedTotalAmount,
            decimal? unapprovedByCorrectionEuAmount,
            decimal? unapprovedByCorrectionBgAmount,
            decimal? unapprovedByCorrectionBfpTotalAmount,
            decimal? unapprovedByCorrectionSelfAmount,
            decimal? unapprovedByCorrectionTotalAmount,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? approvedBfpTotalAmount,
            decimal? approvedSelfAmount,
            decimal? approvedTotalAmount,
            CorrectionType? correctionType,
            int? financialCorrectionId,
            int? irregularityId);

        void ChangeContractReportFinancialCSDBudgetItemStatus(
            int contractReportFinancialCSDBudgetItemId,
            byte[] version,
            ContractReportFinancialCSDBudgetItemStatus status);

        IList<string> CanChangeContractReportFinancialCSDBudgetItemStatusToEnded(int contractReportFinancialCSDBudgetItemId);

        IList<string> CanChangeContractReportFinancialCSDBudgetItemStatusToDraft(int contractReportFinancialCSDBudgetItemId);

        void TechCheckContractReportFinancialCSDBudgetItem(int contractReportFinancialCSDBudgetItemId, byte[] version);

        void UpdateContractReportFinancialEndedCSDs(int oldContractReportFinancialId, int newContractReportFinancialId, int contractReportId);
    }
}
