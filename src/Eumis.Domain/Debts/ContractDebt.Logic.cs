using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Domain.Debts
{
    public partial class ContractDebt
    {
        public void UpdateAttributes(
            DateTime regDate,
            DateTime debtStartDate,
            int? irregularityId,
            int? financialCorrectionId,
            string comment,
            int programmePriorityId,
            int[] paymentIds,
            DateTime? interestStartDate = null)
        {
            this.RegDate = regDate;
            this.DebtStartDate = debtStartDate;
            if (interestStartDate.HasValue)
            {
                this.InterestStartDate = interestStartDate.Value;
            }

            this.IrregularityId = irregularityId;
            this.FinancialCorrectionId = financialCorrectionId;
            this.Comment = comment;
            this.ProgrammePriorityId = programmePriorityId;

            this.ContractDebtPayments.Clear();
            foreach (var paymentId in paymentIds)
            {
                this.ContractDebtPayments.Add(
                    new ContractDebtPayment(paymentId));
            }

            this.ModifyDate = DateTime.Now;
        }

        public void MakeDeleted(string isDeletedNote)
        {
            if (this.Status != ContractDebtStatus.Entered)
            {
                throw new DomainValidationException("ContractDebt status must be Entered in order to mark it as removed.");
            }

            this.Status = ContractDebtStatus.Removed;
            this.IsDeletedNote = isDeletedNote;

            this.ModifyDate = DateTime.Now;
        }

        public void MakeEntered(string regNumber)
        {
            if (this.Status != ContractDebtStatus.New)
            {
                throw new DomainValidationException("ContractDebt status must be New in order to enter it.");
            }

            this.Status = ContractDebtStatus.Entered;
            this.RegNumber = regNumber;
        }
    }
}
