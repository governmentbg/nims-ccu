using System.Collections.Generic;
using Eumis.Data.HistoricContract.ViewObjects;
using Eumis.Domain.HistoricContracts;

namespace Eumis.Data.HistoricContract.Repositories
{
    public interface IHistoricContractRequestRepository : IAggregateRepository<HistoricContractRequest>
    {
        IList<HistoricContractRequestVO> GetHistoricContractRequests();

        HistoricContractRequestInfoVO GetHistoricContractRequestInfo(int historicContractRequestId);
    }
}
