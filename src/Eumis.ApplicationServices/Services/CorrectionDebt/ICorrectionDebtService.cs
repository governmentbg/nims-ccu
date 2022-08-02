using System;
using System.Collections.Generic;
using Eumis.Domain.Debts;

namespace Eumis.ApplicationServices.Services.CorrectionDebt
{
    public interface ICorrectionDebtService
    {
        // Correction debt
        Eumis.Domain.Debts.CorrectionDebt CreateContractDebt(int flatFinancialCorrectionId, DateTime regDate);

        void DeleteDebt(int correctionDebtId, byte[] version);

        // Correction debt version
        IList<string> CanCreateCorrectionDebtVersion(int correctionDebtId);

        CorrectionDebtVersion CreateCorrectionDebtVersion(int correctionDebtId);

        void UpdateCorrectionDebtVersion(
            int correctionDebtVersionId,
            byte[] version,
            decimal? debtEuAmount,
            decimal? debtBgAmount,
            decimal? certEuAmount,
            decimal? certBgAmount,
            decimal? reimbursedEuAmount,
            decimal? reimbursedBgAmount,
            string createNotes);

        void DeleteCorrectionDebtVersion(int correctionDebtVersionId, byte[] version);

        void ChangeCorrectionDebtVersionStatusToActual(int correctionDebtVersionId, byte[] version);
    }
}
