using Eumis.Domain.Debts;
using Eumis.Domain.Debts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Data.Debts.Repositories
{
    public interface ICorrectionDebtsRepository : IAggregateRepository<CorrectionDebt>
    {
        IList<CorrectionDebtVO> GetCorrectionDebts(int[] programmeIds);

        int GetFlatFinancialCorrectionId(int correctionDebtId);

        IList<CorrectionDebtReportVO> GetCorrectionDebtReport(int[] programmeIds);

        IList<CorrectionDebtVO> GetCorrectionDebtsForProjectDossier(int contractId);
    }
}
