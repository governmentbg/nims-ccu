using Eumis.Domain.CertReports;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.CertReport
{
    public interface ICertReportService
    {
        // cert report
        Eumis.Domain.CertReports.CertReport CreateCertReport(
            int programmeId,
            DateTime regDate,
            DateTime dateFrom,
            DateTime dateTo,
            CertReportType type,
            string reportNumber);

        void UpdateCertReport(int certReportId, byte[] version, DateTime regDate, DateTime dateFrom, DateTime dateTo, string reportNumber);

        IList<string> CanDeleteCertReport(int certReportId);

        void DeleteCertReport(int certReportId, byte[] version);

        IList<string> CanChangeCertReportStatusToFinalStatus(int certReportId);

        int? ChangeCertReportStatus(int certReportId, byte[] version, CertReportStatus status, string statusNote = null);

        // cert report documents
        void CreateCertReportContractDebt(int certReportId, byte[] version, int[] contractDebtIds);

        void CreateCertReportDebtReimbursedAmount(int certReportId, byte[] version, int[] debtReimbursedAmountIds);

        void CreateCertReportPayment(int certReportId, byte[] version, int[] contractReportIds);

        void CreateCertReportAdvancePayment(int certReportId, byte[] version, int[] contractReportIds);

        void CreateCertReportFinancialCorrection(int certReportId, byte[] version, int[] contractReportFinancialCorrectionIds);

        void CreateCertReportCorrection(int certReportId, byte[] version, int[] contractReportCorrectionIds);

        void CreateCertReportFinancialRevalidation(int certReportId, byte[] version, int[] contractReportFinancialRevalidationIds);

        void CreateCertReportRevalidation(int certReportId, byte[] version, int[] contractReportRevalidationIds);

        void CreateCertReportFinancialCertCorrection(int certReportId, byte[] version, int[] contractReportFinancialCertCorrectionIds);

        void CreateCertReportCertCorrection(int certReportId, byte[] version, int[] contractReportCertCorrectionIds);

        void DeleteCertReportContractDebt(int certReportId, byte[] version, int contractDebtId);

        void DeleteCertReportDebtReimbursedAmount(int certReportId, byte[] version, int debtReimbursedAmountId);

        void DeleteCertReportPayment(int certReportId, byte[] version, int contractReportId);

        void DeleteCertReportAdvancePayment(int certReportId, byte[] version, int contractReportId);

        void DeleteCertReportFinancialCorrection(int certReportId, byte[] version, int contractReportFinancialCorrectionId);

        void DeleteCertReportCorrection(int certReportId, byte[] version, int contractReportCorrectionId);

        void DeleteCertReportFinancialRevalidation(int certReportId, byte[] version, int contractReportFinancialRevalidationId);

        void DeleteCertReportRevalidation(int certReportId, byte[] version, int contractReportRevalidationId);

        void DeleteCertReportFinancialCertCorrection(int certReportId, byte[] version, int contractReportFinancialCertCorrectionId);

        void DeleteCertReportCertCorrection(int certReportId, byte[] version, int contractReportCertCorrectionId);

        IList<string> CanDeleteCertReportContractDebt(int certReportId, int contractDebtId);

        // cert report documents partial inclusion
        void CreateCertReportPaymentCSDs(int certReportId, byte[] version, int contractReportId, int[] contractReportFinancialCSDBudgetItemIds);

        void CreateCertReportAdvancePaymentAmounts(int certReportId, byte[] version, int contractReportId, int[] contractReportAdvancePaymentAmountIds);

        void CreateCertReportFinancialCorrectionCSDs(int certReportId, byte[] version, int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds);

        void CreateCertReportFinancialRevalidationCSDs(int certReportId, byte[] version, int contractReportFinancialRevalidationId, int[] contractReportFinancialRevalidationCSDIds);

        void CreateCertReportFinancialCertCorrectionCSDs(int certReportId, byte[] version, int contractReportFinancialCertCorrectionId, int[] contractReportFinancialCertCorrectionCSDIds);

        void DeleteCertReportPaymentCSDs(int certReportId, byte[] version, int contractReportId, int[] contractReportFinancialCSDBudgetItemIds);

        void DeleteCertReportAdvancePaymentAmounts(int certReportId, byte[] version, int contractReportId, int[] contractReportAdvancePaymentAmountIds);

        void DeleteCertReportFinancialCorrectionCSDs(int certReportId, byte[] version, int contractReportFinancialCorrectionId, int[] contractReportFinancialCorrectionCSDIds);

        void DeleteCertReportFinancialRevalidationCSDs(int certReportId, byte[] version, int contractReportFinancialRevalidationId, int[] contractReportFinancialRevalidationCSDIds);

        void DeleteCertReportFinancialCertCorrectionCSDs(int certReportId, byte[] version, int contractReportFinancialCertCorrectionId, int[] contractReportFinancialCertCorrectionCSDIds);
    }
}
