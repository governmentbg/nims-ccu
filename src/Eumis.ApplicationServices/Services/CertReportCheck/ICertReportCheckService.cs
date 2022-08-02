using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.CertReportCheck
{
    public interface ICertReportCheckService
    {
        // ContractReportFinancialCSDBudgetItem
        void UpdateContractReportFinancialCSDBudgetItem(
            int certReportId,
            int contractReportFinancialCSDBudgetItemId,
            byte[] version,
            decimal? uncertifiedApprovedEuAmount,
            decimal? uncertifiedApprovedBgAmount,
            decimal? uncertifiedApprovedBfpTotalAmount,
            decimal? uncertifiedApprovedSelfAmount,
            decimal? uncertifiedApprovedTotalAmount,
            decimal? certifiedApprovedEuAmount,
            decimal? certifiedApprovedBgAmount,
            decimal? certifiedApprovedBfpTotalAmount,
            decimal? certifiedApprovedSelfAmount,
            decimal? certifiedApprovedTotalAmount);

        IList<string> CanChangeContractReportFinancialCSDBudgetItemCertStatus(int contractReportFinancialCSDBudgetItemId);

        void ChangeContractReportFinancialCSDBudgetItemCertStatus(int contractReportFinancialCSDBudgetItemId, byte[] version, ContractReportFinancialCSDBudgetItemCertStatus status);

        void CertifyAllContractReportFinancialCSDBudgetItems(int certReportId);

        void CertifyAllContractReportFinancialCSDBudgetItems(int certReportId, int contractReportId);

        void UncertifyAllContractReportFinancialCSDBudgetItems(int certReportId);

        void UncertifyAllContractReportFinancialCSDBudgetItems(int certReportId, int contractReportId);

        // ContractReportFinancialCorrectionCSD
        void UpdateContractReportFinancialCorrectionCSD(
            int certReportId,
            int contractReportFinancialCorrectionCSDId,
            byte[] version,
            decimal? uncertifiedCorrectedApprovedEuAmount,
            decimal? uncertifiedCorrectedApprovedBgAmount,
            decimal? uncertifiedCorrectedApprovedBfpTotalAmount,
            decimal? uncertifiedCorrectedApprovedSelfAmount,
            decimal? uncertifiedCorrectedApprovedTotalAmount,
            decimal? certifiedCorrectedApprovedEuAmount,
            decimal? certifiedCorrectedApprovedBgAmount,
            decimal? certifiedCorrectedApprovedBfpTotalAmount,
            decimal? certifiedCorrectedApprovedSelfAmount,
            decimal? certifiedCorrectedApprovedTotalAmount);

        IList<string> CanChangeContractReportFinancialCorrectionCSDCertStatusToEnded(int contractReportFinancialCorrectionCSDId);

        void ChangeContractReportFinancialCorrectionCSDCertStatus(int contractReportFinancialCorrectionCSDId, byte[] version, ContractReportFinancialCorrectionCSDCertStatus status);

        void CertifyAllContractReportFinancialCorrectionCSDs(int certReportId);

        void CertifyAllContractReportFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId);

        void UncertifyAllContractReportFinancialCorrectionCSDs(int certReportId);

        void UncertifyAllContractReportFinancialCorrectionCSDs(int certReportId, int contractReportFinancialCorrectionId);

        // ContractReportCorrection
        void UpdateContractReportCorrection(
            int certReportId,
            int contractReportCorrectionId,
            byte[] version,
            decimal? uncertifiedCorrectedApprovedEuAmount,
            decimal? uncertifiedCorrectedApprovedBgAmount,
            decimal? uncertifiedCorrectedApprovedBfpTotalAmount,
            decimal? uncertifiedCorrectedApprovedCrossAmount,
            decimal? uncertifiedCorrectedApprovedSelfAmount,
            decimal? uncertifiedCorrectedApprovedTotalAmount,
            decimal? certifiedCorrectedApprovedEuAmount,
            decimal? certifiedCorrectedApprovedBgAmount,
            decimal? certifiedCorrectedApprovedBfpTotalAmount,
            decimal? certifiedCorrectedApprovedCrossAmount,
            decimal? certifiedCorrectedApprovedSelfAmount,
            decimal? certifiedCorrectedApprovedTotalAmount);

        IList<string> CanChangeContractReportCorrectionCertStatusToEnded(int contractReportCorrectionId);

        void ChangeContractReportCorrectionCertStatus(int contractReportCorrectionId, byte[] version, ContractReportCorrectionCertStatus status);

        void CertifyAllContractReportCorrections(int certReportId);

        void UncertifyAllContractReportCorrections(int certReportId);

        // ContractReportAdvancePaymentAmount
        void UpdateContractReportAdvancePaymentAmount(
            int certReportId,
            int contractReportAdvancePaymentAmountId,
            byte[] version,
            decimal? uncertifiedApprovedEuAmount,
            decimal? uncertifiedApprovedBgAmount,
            decimal? uncertifiedApprovedBfpTotalAmount,
            decimal? certifiedApprovedEuAmount,
            decimal? certifiedApprovedBgAmount,
            decimal? certifiedApprovedBfpTotalAmount);

        IList<string> CanChangeContractReportAdvancePaymentAmountCertStatusToEnded(int contractReportAdvancePaymentAmountId);

        void ChangeContractReportAdvancePaymentAmountCertStatus(int contractReportAdvancePaymentAmountId, byte[] version, ContractReportAdvancePaymentAmountCertStatus status);

        void CertifyAllContractReportAdvancePaymentAmounts(int certReportId);

        void CertifyAllContractReportAdvancePaymentAmounts(int certReportId, int contractReportId);

        void UncertifyAllContractReportAdvancePaymentAmounts(int certReportId);

        void UncertifyAllContractReportAdvancePaymentAmounts(int certReportId, int contractReportId);

        // ContractReportFinancialRevalidationCSD
        void UpdateContractReportFinancialRevalidationCSD(
            int certReportId,
            int contractReportFinancialRevalidationCSDId,
            byte[] version,
            decimal? uncertifiedRevalidatedEuAmount,
            decimal? uncertifiedRevalidatedBgAmount,
            decimal? uncertifiedRevalidatedBfpTotalAmount,
            decimal? uncertifiedRevalidatedSelfAmount,
            decimal? uncertifiedRevalidatedTotalAmount,
            decimal? certifiedRevalidatedEuAmount,
            decimal? certifiedRevalidatedBgAmount,
            decimal? certifiedRevalidatedBfpTotalAmount,
            decimal? certifiedRevalidatedSelfAmount,
            decimal? certifiedRevalidatedTotalAmount);

        IList<string> CanChangeContractReportFinancialRevalidationCSDCertStatusToEnded(int contractReportFinancialRevalidationCSDId);

        void ChangeContractReportFinancialRevalidationCSDCertStatus(int contractReportFinancialRevalidationCSDId, byte[] version, ContractReportFinancialRevalidationCSDCertStatus status);

        void CertifyAllContractReportFinancialRevalidationCSDs(int certReportId);

        void CertifyAllContractReportFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId);

        void UncertifyAllContractReportFinancialRevalidationCSDs(int certReportId);

        void UncertifyAllContractReportFinancialRevalidationCSDs(int certReportId, int contractReportFinancialRevalidationId);

        // ContractReportRevalidation
        void UpdateContractReportRevalidation(
            int certReportId,
            int contractReportRevalidationId,
            byte[] version,
            decimal? uncertifiedRevalidatedEuAmount,
            decimal? uncertifiedRevalidatedBgAmount,
            decimal? uncertifiedRevalidatedBfpTotalAmount,
            decimal? uncertifiedRevalidatedCrossAmount,
            decimal? uncertifiedRevalidatedSelfAmount,
            decimal? uncertifiedRevalidatedTotalAmount,
            decimal? certifiedRevalidatedEuAmount,
            decimal? certifiedRevalidatedBgAmount,
            decimal? certifiedRevalidatedBfpTotalAmount,
            decimal? certifiedRevalidatedCrossAmount,
            decimal? certifiedRevalidatedSelfAmount,
            decimal? certifiedRevalidatedTotalAmount);

        IList<string> CanChangeContractReportRevalidationCertStatusToEnded(int contractReportRevalidationId);

        void ChangeContractReportRevalidationCertStatus(int contractReportRevalidationId, byte[] version, ContractReportRevalidationCertStatus status);

        void CertifyAllContractReportRevalidations(int certReportId);

        void UncertifyAllContractReportRevalidations(int certReportId);
    }
}
