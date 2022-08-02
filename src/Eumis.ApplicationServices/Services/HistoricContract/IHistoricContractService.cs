using Eumis.Domain.HistoricContracts.DataObjects;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.HistoricContract
{
    public interface IHistoricContractService
    {
        IList<HistoricContractErrorDO> UpdateHistoricContracts(List<HistoricContractDO> historicContracts);

        void CanUpdate(List<HistoricContractDO> historicContracts);

        void EditHistoricContracts(List<HistoricContractDO> historicContracts);
    }
}
