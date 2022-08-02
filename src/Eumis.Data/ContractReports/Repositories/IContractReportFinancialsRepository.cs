using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportFinancialsRepository : IAggregateRepository<ContractReportFinancial>
    {
        IList<ContractReportFinancial> FindAll(int contractReportId);

        Task<IList<ContractReportFinancial>> FindAllAsync(int contractReportId, CancellationToken ct);

        ContractReportFinancial Find(Guid gid);

        Task<ContractReportFinancial> FindAsync(Guid gid, CancellationToken ct);

        ContractReportFinancial GetActualContractReportFinancial(int contractReportId);

        Task<ContractReportFinancial> GetActualContractReportFinancialAsync(int contractReportId, CancellationToken ct);

        int GetNextVersionNum(int contractId);

        Task<int> GetNextVersionNumAsync(int contractId, CancellationToken ct);

        int GetNextVersionSubNum(int contractReportId);

        Task<int> GetNextVersionSubNumAsync(int contractReportId, CancellationToken ct);

        IList<ContractReportFinancialVO> GetContractReportFinancials(int contractReportId);

        ContractReportFinancial GetLastContractReportFinancial(int contractId);

        Task<ContractReportFinancial> GetLastContractReportFinancialAsync(int contractId, CancellationToken ct);

        Task<ContractReportFinancial> GetLastContractReportFinancialAsync(Guid contractReportGid, CancellationToken ct);
    }
}
