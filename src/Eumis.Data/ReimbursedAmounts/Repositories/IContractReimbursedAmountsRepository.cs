using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ReimbursedAmounts.Repositories
{
    public interface IContractReimbursedAmountsRepository : IAggregateRepository<ContractReimbursedAmount>
    {
        IList<ContractReimbursedAmountVO> GetReimbursedAmounts(int[] programmeIds, int userId, int? contractId = null, ReimbursementType? type = null);

        ContractReimbursedAmountInfoVO GetInfo(int reimbursedAmountId);

        ContractReimbursedAmountBasicDataVO GetBasicData(int reimbursedAmountId);

        int GetProgrammeId(int reimbursedAmountId);

        IList<ContractReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int contractId);

        void SwitchToDebtReimbursedAmount(int reimbursedAmountId, int contractDebtId);

        int GetContractId(int reimbursedAmountId);
    }
}
