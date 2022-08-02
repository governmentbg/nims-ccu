using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReportAdvancePaymentAmounts.Repositories
{
    public interface IContractReportAdvancePaymentAmountsRepository : IAggregateRepository<ContractReportAdvancePaymentAmount>
    {
        IList<ContractReportAdvancePaymentAmount> FindAll(int contractReportId);

        IList<ContractReportAdvancePaymentAmount> FindAll(int contractReportId, int[] contractReportAdvancePaymentAmountIds);

        IList<ContractReportAdvancePaymentAmount> FindAllByCertReport(int certReportId, int contractReportId);

        IList<ContractReportAdvancePaymentAmount> FindAllByCertReport(int certReportId);

        IList<ContractReportAdvancePaymentAmountsVO> GetContractReportAdvancePaymentAmounts(int contractReportId, bool? isAttachedToCertReport = null, int? certReportId = null);

        Task<IList<ContractReportAdvancePaymentAmountsVO>> GetContractReportAdvancePaymentAmountsAsync(int contractReportId, CancellationToken ct, bool? isAttachedToCertReport = null, int? certReportId = null);

        bool HasDraftContractReportAdvancePaymentAmounts(int contractReportId);

        bool HasCertDraftContractReportAdvancePaymentAmounts(int certReportId);

        bool HasCertContractReportAdvancePaymentAmounts(int certReportId);
    }
}
