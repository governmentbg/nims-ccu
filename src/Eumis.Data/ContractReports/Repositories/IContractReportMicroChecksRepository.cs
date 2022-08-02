using Eumis.Domain.Contracts.ContractReportMicros;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportMicroChecksRepository : IAggregateRepository<ContractReportMicroCheck>
    {
        IList<ContractReportMicroCheckVO> GetContractReportMicroChecks(int contractReportId);

        IList<ContractReportMicroCheck> FindAll(int contractReportId, ContractReportMicroType type);

        int GetNextOrderNum(int contractReportMicroId);

        bool HasContractReportMicroCheckInProgress(int contractReportId, ContractReportMicroType type);

        ContractReportMicroCheck GetActualContractReportMicroCheck(int contractReportId, ContractReportMicroType type);
    }
}
