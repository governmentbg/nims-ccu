using Eumis.Data.ContractReports.ViewObjects.ContractBudgetTree;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportsRepository : IAggregateRepository<ContractReport>
    {
        ContractReport Find(Guid gid);

        Task<ContractReport> FindAsync(Guid gid, CancellationToken ct);

        Task<ContractReport> FindForUpdateAsync(Guid gid, byte[] version, CancellationToken ct);

        ContractReport FindByNum(int contractId, string contractReportNum);

        int GetContractId(int contractReportId);

        int GetContractReportId(Guid gid);

        int GetNextOrderNumber(int contractId);

        Task<int> GetNextOrderNumberAsync(int contractId, CancellationToken ct);

        IList<ContractReportVO> GetContractReports(
            int[] programmeIds,
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null);

        IList<ContractReportVO> GetContractReportChecksContractReports(int[] programmeIds, int userId, string contractRegNum = null);

        IList<ContractReportExcelVO> GetContractReportChecksContractReportsExcelExport(int[] programmeIds, string contractRegNum = null);

        Task<PagePVO<ContractReportPVO>> GetPortalContractReportsAsync(Guid contractGid, CancellationToken ct, int offset = 0, int? limit = null);

        bool CanCreate(int contractId);

        Task<bool> CanCreateAsync(int contractId, CancellationToken ct);

        bool HasContractReportInProgress(int contractId);

        Task<bool> HasContractReportInProgressAsync(int contractId, CancellationToken ct);

        bool HasContractReportInProgress(int contractId, int contractReportId);

        Task<bool> HasContractReportInProgressAsync(int contractId, int contractReportId, CancellationToken ct);

        bool HasContractReportDraft(int contractId, int contractReportId);

        Task<bool> HasContractReportDraftAsync(int contractId, int contractReportId, CancellationToken ct);

        bool HasAdvanceVerificationPayment(int contractId, int contractReportId);

        Task<bool> HasAdvanceVerificationPaymentAsync(int contractId, int contractReportId, CancellationToken ct);

        bool IsContractReportNumExisting(string contractReportNum);

        IList<ContractReportFinancialCorrectionVO> GetContractReportAttachedFinancialCorrections(int contractReportId);

        IList<ContractReportFinancialCorrectionVO> GetFinancialCorrectionsForContractReport(int contractId);

        IList<ContractReportFinancialCorrectionCSDsVO> GetContractReportAttachedFinancialCorrectionSignCorrectedAmounts(int contractReportId);

        Task<IList<ContractReportFinancialCorrectionCSDsVO>> GetContractReportAttachedFinancialCorrectionSignCorrectedAmountsAsync(int contractReportId, CancellationToken ct);

        ContractBudgetTreeVO GetContractReportPaymentRequests(int contractReportId);

        IList<ContractReportVO> GetFinancialCorrectionContractReports(int financialCorrectionId);

        IList<ContractReportVO> GetContractReportsForFinancial(
            int[] programmeIds,
            string contractRegNum = null,
            int? procedureId = null,
            string contractReportNum = null,
            int? userId = null);

        IList<ContractReportVO> GetContractReportsForTechnical(
                    int[] programmeIds,
                    int userId,
                    string contractRegNum = null,
                    int? procedureId = null,
                    string contractReportNum = null);

        IList<ContractReportRequestedAmountVO> GetContractReportRequestedAmountsForProjectDossier(int contractId);

        IList<ContractReportApprovedAmountVO> GetContractReportApprovedAmountsForProjectDossier(int contractId);

        IList<ContractReportCertifiedAmountVO> GetContractReportCertifiedAmountsForProjectDossier(int contractId);

        IList<ContractReportVO> GetContractReportWithTechnicalForProjectDossier(int contractId);

        IList<string> CanReturnContractReportStatusToUnchecked(int contractReportId);

        IList<ContractReportSAPDataVO> GetContractReportSAPData(int contractReportId);

        bool HasReturnedContractReportDocuments(int contractReportId);

        Task<bool> HasReturnedContractReportDocumentsAsync(int contractReportId, CancellationToken ct);

        bool ContractReportHasFinancial(int contractReportId);

        Task<bool> ContractReportHasFinancialAsync(int contractReportId, CancellationToken ct);

        bool ContractReportHasTechnical(int contractReportId);

        Task<bool> ContractReportHasTechnicalAsync(int contractReportId, CancellationToken ct);

        bool ContractReportHasPayment(int contractReportId);

        Task<bool> ContractReportHasPaymentAsync(int contractReportId, CancellationToken ct);

        bool ContractReportHasAdvancePayment(int contractReportId);

        Task<bool> ContractReportHasAdvancePaymentAsync(int contractReportId, CancellationToken ct);

        bool CanEditSentContractReport(int contractId);

        Task<bool> CanEditSentContractReportAsync(int contractId, CancellationToken ct);

        bool CanChangeContractReportStatusToUnchecked(int contractReportId);

        List<ContractReport> GetPreviousContractReport(int contractReportId);

        int GetContractReportProcedureId(int contractReportId);

        Task<int> GetContractReportProcedureIdAsync(int contractReportId, CancellationToken ct);
    }
}
