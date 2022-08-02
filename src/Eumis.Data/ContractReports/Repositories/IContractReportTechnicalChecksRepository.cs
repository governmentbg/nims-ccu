using Eumis.Domain.Contracts;
using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eumis.Data.ContractReports.Repositories
{
    public interface IContractReportTechnicalChecksRepository : IAggregateRepository<ContractReportTechnicalCheck>
    {
        IList<ContractReportTechnicalCheckVO> GetContractReportTechnicalChecks(int contractReportId);

        IList<ContractReportTechnicalCheck> FindAll(int contractReportId);

        int GetNextOrderNum(int contractReportTechnicalId);

        bool HasContractReportTechnicalCheckInProgress(int contractReportId);

        ContractReportTechnicalCheck GetActualContractReportTechnicalCheck(int contractReportId);
    }
}
