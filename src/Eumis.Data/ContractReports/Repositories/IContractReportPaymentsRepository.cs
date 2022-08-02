using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportPaymentsRepository : IAggregateRepository<ContractReportPayment>
    {
        IList<ContractReportPayment> FindAll(int contractReportId);

        Task<IList<ContractReportPayment>> FindAllAsync(int contractReportId, CancellationToken ct);

        ContractReportPayment Find(Guid gid);

        ContractReportPayment GetActualContractReportPayment(int contractReportId);

        IList<Tuple<int, int, int>> GetActualContractReportPaymentsData(int[] contractIds);

        int GetNextVersionNum(int contractId);

        Task<int> GetNextVersionNumAsync(int contractId, CancellationToken ct);

        int GetNextVersionSubNum(int contractReportId);

        Task<int> GetNextVersionSubNumAsync(int contractReportId, CancellationToken ct);

        IList<ContractReportPaymentVO> GetContractReportPayments(int contractReportId);

        IList<ActuallyPaidAmountContractReportPaymentVO> GetActuallyPaidAmountContractReportPayments(int contractId);

        ContractReportPayment GetLastAdvanceVerificationContractReportPayment(int contractId);

        Task<ContractReportPayment> GetLastAdvanceVerificationContractReportPaymentAsync(int contractId, CancellationToken ct);

        bool CanCreateAdvanceVerificationPayment(int contractId);

        ContractReportStatus GetContractReportStatus(int paymentId);

        ContractReportPaymentStatus GetContractReportPaymentStatus(int paymentId);

        int GetContractId(int paymentId);
    }
}
