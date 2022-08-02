using Eumis.Domain.Debts;
using Eumis.Domain.Debts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.Debts.Repositories
{
    public interface ICorrectionDebtVersionsRepository : IAggregateRepository<CorrectionDebtVersion>
    {
        IList<CorrectionDebtVersionVO> GetCorrectionDebtVersions(int correctionDebtId);

        bool HasCorrectionDebtVersionsInProgress(int correctionDebtId);

        bool HasNonDraftCorrectionDebtVersions(int correctionDebtId);

        CorrectionDebtVersion GetActualVersion(int correctionDebtId);

        void RemoveByDebtId(int correctionDebtId);
    }
}
