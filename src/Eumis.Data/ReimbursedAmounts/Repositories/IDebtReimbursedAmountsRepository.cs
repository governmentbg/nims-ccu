using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.ReimbursedAmounts.Repositories
{
    public interface IDebtReimbursedAmountsRepository : IAggregateRepository<DebtReimbursedAmount>
    {
        IList<DebtReimbursedAmountVO> GetReimbursedAmounts(int[] programmeIds, int userId, int? contractId = null, ReimbursementType? type = null);

        IList<DebtReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int contractId);

        IList<DebtReimbursedAmount> FindAllEnteredForDebt(int contractDebtId);

        IList<DebtReimbursedAmount> FindAllForDebt(int contractDebtId);

        DebtReimbursedAmountInfoVO GetInfo(int reimbursedAmountId);

        DebtReimbursedAmountBasicDataVO GetBasicData(int reimbursedAmountId);

        int GetProgrammeId(int reimbursedAmountId);

        IList<string> CanEnter(int reimbursedAmountId);

        IList<string> CanSetToDraft(int reimbursedAmountId);

        int GetContractId(int reimbursedAmountId);
    }
}
