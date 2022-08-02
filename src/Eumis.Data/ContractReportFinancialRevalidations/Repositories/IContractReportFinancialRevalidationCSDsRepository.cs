using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReportFinancialRevalidations.Repositories
{
    public interface IContractReportFinancialRevalidationCSDsRepository : IAggregateRepository<ContractReportFinancialRevalidationCSD>
    {
        IList<ContractReportFinancialRevalidationCSD> FindAll(int contractReportFinancialRevalidationId);

        IList<ContractReportFinancialRevalidationCSD> FindAll(int contractReportFinancialRevalidationId, int[] contractReportFinancialRevalidationCSDIds);

        IList<ContractReportFinancialRevalidationCSD> FindAllUnattached(int contractReportFinancialRevalidationId);

        IList<ContractReportFinancialRevalidationCSD> FindAllByCertReport(int certReportId, int contractReportFinancialRevalidationId);

        IList<ContractReportFinancialRevalidationCSD> FindAllByCertReport(int certReportId);

        IList<ContractReportFinancialRevalidationCSDsVO> GetContractReportFinancialRevalidationCSDs(
            int contractReportFinancialRevalidationId,
            string csd = null,
            string company = null,
            bool? isAttachedToCertReport = null,
            int? certReportId = null);

        IList<ContractReportFinancialRevalidationCSDsVO> GetContractReportFinancialRevalidationCSDsForContractReportRevalidationCertAuthorityFinancialCorrection(
            int contractReportId,
            int contractReportRevalidationCertAuthorityFinancialCorrectionId,
            string csdFilter,
            string company);

        ContractReportFinancialRevalidationCSD GetContractReportFinancialRevalidationCSDByBudgetItem(int contractReportId, int contractReportFinancialCSDBudgetItemId);

        bool HasContractReportFinancialRevalidationCSDs(int contractReportFinancialRevalidationId);

        bool HasDraftContractReportFinancialRevalidationCSDs(int contractReportFinancialRevalidationId);

        bool HasCertDraftContractReportFinancialRevalidationCSDs(int certReportId);

        bool HasCertContractReportFinancialRevalidationCSDs(int certReportId);
    }
}
