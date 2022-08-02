using Eumis.Data.FIReimbursedAmounts.ViewObjects;
using Eumis.Domain.FIReimbursedAmounts;
using System.Collections.Generic;

namespace Eumis.Data.FIReimbursedAmounts.Repositories
{
    public interface IFIReimbursedAmountsRepository : IAggregateRepository<FIReimbursedAmount>
    {
        IList<FIReimbursedAmountVO> GetReimbursedAmounts(int[] programmeIds, int? contractId = null, FIReimbursementType? type = null);

        IList<FIReimbursedAmountVO> GetReimbursedAmountsForProjectDossier(int contractId);

        FIReimbursedAmountInfoVO GetInfo(int fiReimbursedAmountId);

        FIReimbursedAmountBasicDataVO GetBasicData(int fiReimbursedAmountId);

        int GetProgrammeId(int fiReimbursedAmountId);
    }
}
