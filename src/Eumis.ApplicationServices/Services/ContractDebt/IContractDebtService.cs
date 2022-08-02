using System;
using System.Collections.Generic;
using Eumis.Domain.Debts;
using Eumis.Domain.NonAggregates;

namespace Eumis.ApplicationServices.Services.ContractDebt
{
    public interface IContractDebtService
    {
        // contract debt
        Eumis.Domain.Debts.ContractDebt CreateContractDebt(
            int contractId,
            int programmePriorityId,
            DateTime regDate,
            DateTime debtStartDate,
            DateTime interestStartDate,
            int[] paymentIds);

        IList<string> CanCreate(string contractRegNumber);

        void UpdateContractDebt(
            int contractDebtId,
            byte[] version,
            DateTime regDate,
            DateTime debtStartDate,
            DateTime interestStartDate,
            int? irregularityId,
            int? financialCorrectionId,
            string comment,
            int programmePriorityId,
            int[] paymentIds);

        void DeleteContractDebt(int contractDebtId, byte[] version);

        // contract debt version
        IList<string> CanCreateContractDebtVersion(int contractDebtId);

        ContractDebtVersion CreateContractDebtVersion(int contractDebtId);

        bool CanUpdateContractDebtVersion(ContractDebtVersion version);

        bool CanDeleteContractDebtVersion(ContractDebtVersion version);

        void ChangeContractDebtVersionStatusToActual(int contractDebtVersionId, byte[] version);

        // contract debt interest
        ContractDebtInterest CreateContractDebtInterest(
            int contractDebtId,
            byte[] version,
            int interestSchemeId,
            DateTime dateTo,
            decimal euInterestAmount,
            decimal bgInterestAmount,
            decimal totalInterestAmount,
            decimal euAmount,
            decimal bgAmount,
            decimal totalAmount);

        void UpdateContractDebtInterest(
            int contractDebtId,
            byte[] version,
            int contractDebtInterestId,
            int interestSchemeId,
            DateTime dateTo,
            decimal euInterestAmount,
            decimal bgInterestAmount,
            decimal totalInterestAmount,
            decimal euAmount,
            decimal bgAmount,
            decimal totalAmount);

        void DeleteContractDebtInterest(int contractDebtId, byte[] version, int contractDebtInterestId);
    }
}
