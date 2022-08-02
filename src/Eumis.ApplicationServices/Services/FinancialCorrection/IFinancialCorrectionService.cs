using System;
using System.Collections.Generic;
using Eumis.Domain.MonitoringFinancialControl.FinancialCorrections;

namespace Eumis.ApplicationServices.Services.FinancialCorrection
{
    public interface IFinancialCorrectionService
    {
        // financial correction
        Eumis.Domain.MonitoringFinancialControl.FinancialCorrections.FinancialCorrection CreateFinancialCorrection(
            DateTime impositionDate,
            int contractId,
            int? contractContractId,
            int? contractBudgetLevel3AmountId,
            int userId);

        IList<string> CanCreate(string contractRegNumber, int userId);

        void DeleteFinancialCorrection(int financialCorrectionId, byte[] version);

        // financial correction version
        IList<string> CanCreateFinancialCorrectionVersion(int financialCorrectionId);

        bool CanModifyFinancialCorrectionVersion(int financialCorrectionVersionId);

        FinancialCorrectionVersion CreateFinancialCorrectionVersion(int financialCorrectionId);

        FinancialCorrectionVersion ChangeFinancialCorrectionVersionStatusToActual(int flatFinancialCorrectionVersionId, byte[] version);
    }
}
