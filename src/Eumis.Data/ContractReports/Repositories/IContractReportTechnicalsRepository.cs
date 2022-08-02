using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportTechnicalsRepository : IAggregateRepository<ContractReportTechnical>
    {
        IList<ContractReportTechnical> FindAll(int contractReportId);

        Task<IList<ContractReportTechnical>> FindAllAsync(int contractReportId, CancellationToken ct);

        ContractReportTechnical Find(Guid gid);

        Task<ContractReportTechnical> FindAsync(Guid gid, CancellationToken ct);

        ContractReportTechnical GetActualContractReportTechnical(int contractReportId);

        int GetNextVersionNum(int contractId);

        Task<int> GetNextVersionNumAsync(int contractId, CancellationToken ct);

        int GetNextVersionSubNum(int contractReportId);

        Task<int> GetNextVersionSubNumAsync(int contractReportId, CancellationToken ct);

        IList<ContractReportTechnicalVO> GetContractReportTechnicals(int contractReportId);

        ContractReportTechnical GetLastContractReportTechnical(int contractId);

        Task<ContractReportTechnical> GetLastContractReportTechnicalAsync(int contractId, CancellationToken ct);
    }
}
