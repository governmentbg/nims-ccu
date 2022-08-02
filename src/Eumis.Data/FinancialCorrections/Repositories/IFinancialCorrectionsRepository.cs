using System.Collections.Generic;
using Eumis.Data.FinancialCorrections.ViewObjects;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;

namespace Eumis.Data.FinancialCorrections.Repositories
{
    public interface IFinancialCorrectionsRepository : IAggregateRepository<FinancialCorrection>
    {
        IList<FinancialCorrectionVO> GetFinancialCorrections(int[] programmeIds, int userId);

        FinancialCorrectionInfoVO GetInfo(int financialCorrectionId);

        int GetNextOrderNumber(int cotractId);

        int GetContractId(int financialCorrectionId);

        IList<FinancialCorrectionVO> GetFinancialCorrectionsForProjectDossier(int contractId);
    }
}
