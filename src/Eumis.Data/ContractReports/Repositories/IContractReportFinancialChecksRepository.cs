using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportFinancialChecksRepository : IAggregateRepository<ContractReportFinancialCheck>
    {
        IList<ContractReportFinancialCheckVO> GetContractReportFinancialChecks(int contractReportId);

        IList<ContractReportFinancialCheck> FindAll(int contractReportId);

        Task<IList<ContractReportFinancialCheck>> FindAllAsync(int contractReportId, CancellationToken ct);

        int GetNextOrderNum(int contractReportFinancialId);

        bool HasContractReportFinancialCheckInProgress(int contractReportId);

        ContractReportFinancialCheck GetActualContractReportFinancialCheck(int contractReportId);
    }
}
