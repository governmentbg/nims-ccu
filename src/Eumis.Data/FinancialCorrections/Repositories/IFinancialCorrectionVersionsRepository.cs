using System.Collections.Generic;
using Eumis.Data.FinancialCorrections.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;

namespace Eumis.Data.FinancialCorrections.Repositories
{
    public interface IFinancialCorrectionVersionsRepository : IAggregateRepository<FinancialCorrectionVersion>
    {
        IList<FinancialCorrectionVersionVO> GetFinancialCorrectionVersions(int financialCorrectionId);

        bool HasFinancialCorrectionVersionsInProgress(int financialCorrectionId);

        bool HasNonDraftFinancialCorrectionVersions(int financialCorrectionId);

        FinancialCorrectionVersion GetActualVersion(int financialCorrectionId);

        FinancialCorrectionStatus GetFinancialCorrectionStatus(int versionId);

        void RemoveByFinancialCorrectionId(int financialCorrectionId);
    }
}
