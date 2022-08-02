using Eumis.Domain.MonitoringFinancialControl.ReimbursedAmounts;
using Eumis.Domain.NonAggregates;
using System;

namespace Eumis.Domain.FIReimbursedAmounts
{
    public partial class FIReimbursedAmount
    {
        public void UpdateData(
            DateTime reimbursementDate,
            FIReimbursementType type,
            Reimbursement reimbursement,
            decimal? principalBfpEuAmount,
            decimal? principalBfpBgAmount,
            decimal? interestBfpEuAmount,
            decimal? interestBfpBgAmount,
            string comment,
            int? programmePriorityId)
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
            this.ProgrammePriorityId = programmePriorityId;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateEuAmounts(decimal? principalBfpEuAmount, decimal? interestBfpEuAmount)
        {
            this.PrincipalBfp.SetAttributes(principalBfpEuAmount, this.PrincipalBfp.BgAmount);
            this.InterestBfp.SetAttributes(interestBfpEuAmount, this.InterestBfp.BgAmount);
        }

        public void UpdateBgAmounts(decimal? principalBfpBgAmount, decimal? interestBfpBgAmount)
        {
            this.PrincipalBfp.SetAttributes(this.PrincipalBfp.EuAmount, principalBfpBgAmount);
            this.InterestBfp.SetAttributes(this.InterestBfp.EuAmount, interestBfpBgAmount);
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = FIReimbursedAmountStatus.Draft;
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

            this.Status = FIReimbursedAmountStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDeleted(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("FIReimbursedAmount must be activated!");
            }

            this.Status = FIReimbursedAmountStatus.Deleted;
            this.IsDeletedNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != FIReimbursedAmountStatus.Draft)
            {
                throw new DomainValidationException("FIReimbursedAmount status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != FIReimbursedAmountStatus.Entered)
            {
                throw new DomainValidationException("FIReimbursedAmount status must be entered!");
            }
        }
    }
}
