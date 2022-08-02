using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.HistoricContract.ViewObjects;
using Eumis.Domain.HistoricContracts;

namespace Eumis.Data.HistoricContract.Repositories
{
    internal class HistoricContractRequestRepository : AggregateRepository<HistoricContractRequest>, IHistoricContractRequestRepository
    {
        public HistoricContractRequestRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<HistoricContractRequestVO> GetHistoricContractRequests()
        {
            return (from hcr in this.unitOfWork.DbContext.Set<HistoricContractRequest>()
                    orderby hcr.CreateDate descending
                    select new HistoricContractRequestVO
                    {
                        HistoricContractRequestId = hcr.HistoricContractRequestId,
                        CreateDate = hcr.CreateDate,
                        StatusCode = hcr.StatusCode,
                        CountContracts = hcr.CountContracts,
                    }).ToList();
        }

        public HistoricContractRequestInfoVO GetHistoricContractRequestInfo(int historicContractRequestId)
        {
            return (from hcr in this.unitOfWork.DbContext.Set<HistoricContractRequest>()
                    where hcr.HistoricContractRequestId == historicContractRequestId
                    select new HistoricContractRequestInfoVO
                    {
                        HistoricContractRequestId = hcr.HistoricContractRequestId,
                        CreateDate = hcr.CreateDate,
                        EndDate = hcr.EndDate,
                        StatusCode = hcr.StatusCode,
                        ErrorMessage = hcr.ErrorMessage,
                        CountContracts = hcr.CountContracts,
                        Json = hcr.Json,
                    }).Single();
        }
    }
}
