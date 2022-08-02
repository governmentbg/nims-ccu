using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts
{
    public partial class ReimbursedAmount
    {
        public void UpdateData(
            DateTime reimbursementDate,
            ReimbursementType type,
            Reimbursement reimbursement,
            decimal? principalBfpEuAmount,
            decimal? principalBfpBgAmount,
            decimal? interestBfpEuAmount,
            decimal? interestBfpBgAmount,
            string comment)
        {
            this.AssertIsDraft();

            this.ReimbursementDate = reimbursementDate;
            this.Type = type;
            this.Reimbursement = reimbursement;

            if (reimbursement == Reimbursement.Bank)
            {
                this.ShouldInfluencePaidAmounts = true;
            }
            else if (reimbursement == Reimbursement.Deduction)
            {
                this.ShouldInfluencePaidAmounts = false;
            }
            else if (reimbursement == Reimbursement.DistributedLimitDeduction)
            {
                this.ShouldInfluencePaidAmounts = true;
            }

            this.PrincipalBfp.SetAttributes(principalBfpEuAmount, principalBfpBgAmount);
            this.InterestBfp.SetAttributes(interestBfpEuAmount, interestBfpBgAmount);

            this.Comment = comment;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateContractData(
            DateTime reimbursementDate,
            ReimbursementType type,
            Reimbursement reimbursement,
            decimal? principalBfpEuAmount,
            decimal? principalBfpBgAmount,
            decimal? interestBfpEuAmount,
            decimal? interestBfpBgAmount,
            string comment,
            int programmePriorityId,
            int[] paymentIds)
        {
            this.UpdateData(
                reimbursementDate,
                type,
                reimbursement,
                principalBfpEuAmount,
                principalBfpBgAmount,
                interestBfpEuAmount,
                interestBfpBgAmount,
                comment);

            this.ProgrammePriorityId = programmePriorityId;

            ((ContractReimbursedAmount)this).ContractReimbursedAmountPayments.Clear();
            foreach (var paymentId in paymentIds)
            {
                ((ContractReimbursedAmount)this).ContractReimbursedAmountPayments.Add(
                    new ContractReimbursedAmountPayment(paymentId));
            }
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = ReimbursedAmountStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered(string regNumber = null)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                this.IsActivated = true;
                this.RegNumber = regNumber;
            }

            this.Status = ReimbursedAmountStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDeleted(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("ReimbursedAmount must be activated!");
            }

            this.Status = ReimbursedAmountStatus.Deleted;
            this.IsDeletedNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != ReimbursedAmountStatus.Draft)
            {
                throw new DomainValidationException("ReimbursedAmount status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != ReimbursedAmountStatus.Entered)
            {
                throw new DomainValidationException("ReimbursedAmount status must be entered!");
            }
        }
    }
}
