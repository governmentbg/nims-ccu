using Eumis.Data.ContractReportFinancialCSDs.PortalViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReportFinancialCSDs.Repositories
{
    public interface IContractReportFinancialCSDBudgetItemsRepository : IAggregateRepository<ContractReportFinancialCSDBudgetItem>
    {
        IList<ContractReportFinancialCSDBudgetItem> FindAll(int contractReportId);

        IList<ContractReportFinancialCSDBudgetItem> FindAll(int contractReportId, int[] contractReportFinancialCSDBudgetItemIds);

        IList<ContractReportFinancialCSDBudgetItem> FindAllUnattached(int contractReportId);

        IList<ContractReportFinancialCSDBudgetItem> FindAllByCertReport(int certReportId, int contractReportId);

        IList<ContractReportFinancialCSDBudgetItem> FindAllByCertReport(int certReportId);

        IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItems(
            int contractReportId,
            string csd = null,
            string company = null,
            bool? isAttachedToCertReport = null,
            int? certReportId = null);

        IList<ContractReportFinancialCSDsVO> GetContractReportFinancialCSDsForProjectDossier(int contractId);

        Task<IList<ContractReportFinancialCSDBudgetItemsVO>> GetContractReportFinancialCSDBudgetItemsAsync(
            int contractReportId,
            CancellationToken ct,
            string csdFilter = null,
            string company = null,
            bool? isAttachedToCertReport = null,
            int? certReportId = null);

        bool HasDraftContractReportFinancialCSDBudgetItem(int contractReportId);

        IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportFinancialCorrection(
            int contractReportId,
            int contractReportFinancialCorrectionId,
            string csd = null,
            string company = null);

        IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportFinancialRevalidation(
            int contractReportId,
            int contractReportFinancialRevalidationId,
            string csd = null,
            string company = null);

        IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportFinancialCertCorrection(
            int contractReportId,
            int contractReportFinancialCertCorrectionId,
            string csd = null,
            string company = null);

        IList<ContractReportFinancialCSDBudgetItemsVO> GetContractReportFinancialCSDBudgetItemsForContractReportCertAuthorityFinancialCorrection(
            int contractReportId,
            int contractReportCertAuthorityFinancialCorrectionId,
            string csd = null,
            string company = null);

        bool HasCertDraftContractReportFinancialCSDBudgetItems(int certReportId);

        bool HasCertContractReportFinancialCSDBudgetItems(int certReportId);

        IList<ContractReportFinancialCSDBudgetItemPVO> GetPortalContractReportFinancialCSDBudgetItems(int contractId);

        Task<IList<ContractReportFinancialCSDBudgetItemPVO>> GetPortalContractReportFinancialCSDBudgetItemsAsync(int contractId, CancellationToken ct);
    }
}
