using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportPaymentChecksRepository : IAggregateRepository<ContractReportPaymentCheck>
    {
        IList<ContractReportPaymentCheckVO> GetContractReportPaymentChecks(int contractReportId);

        IList<ContractReportPaymentCheck> FindAll(int contractReportId);

        Task<IList<ContractReportPaymentCheck>> FindAllAsync(int contractReportId, CancellationToken ct);

        int GetNextOrderNum(int contractReportPaymentId);

        bool HasContractReportPaymentCheckInProgress(int contractReportId);

        ContractReportPaymentCheck GetActualContractReportPaymentCheck(int contractReportId);
    }
}
