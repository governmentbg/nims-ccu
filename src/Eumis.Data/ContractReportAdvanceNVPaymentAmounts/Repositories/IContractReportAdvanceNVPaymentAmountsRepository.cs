using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReportAdvanceNVPaymentAmounts.Repositories
{
    public interface IContractReportAdvanceNVPaymentAmountsRepository : IAggregateRepository<ContractReportAdvanceNVPaymentAmount>
    {
        IList<ContractReportAdvanceNVPaymentAmount> FindAll(int contractReportId);

        IList<ContractReportAdvanceNVPaymentAmount> FindAll(int contractReportId, int[] contractReportAdvanceNVPaymentAmountIds);

        IList<ContractReportAdvanceNVPaymentAmountsVO> GetContractReportAdvanceNVPaymentAmounts(int contractReportId);

        Task<IList<ContractReportAdvanceNVPaymentAmountsVO>> GetContractReportAdvanceNVPaymentAmountsAsync(int contractReportId, CancellationToken ct);

        bool HasDraftContractReportAdvanceNVPaymentAmounts(int contractReportId);
    }
}
