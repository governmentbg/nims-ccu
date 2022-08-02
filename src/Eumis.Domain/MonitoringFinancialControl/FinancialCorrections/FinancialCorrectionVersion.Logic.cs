using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.MonitoringFinancialControl.FinancialCorrections
{
    public partial class FinancialCorrectionVersion
    {
        public void UpdateAttributes(
            decimal? percent,
            decimal? euAmount,
            decimal? bgAmount,
            decimal? bfpAmount,
            decimal? selfAmount,
            decimal? totalAmount,
            int? financialCorrectionImposingReasonId,
            string description,
            FinancialCorrectionVersionViolationFoundBy? violationFoundBy,
            AmendmentReason? amendmentReason,
            CorrectionBearer? correctionBearer,
            Guid? blobKey,
            int[] violationIds)
        {
            this.AssertIsDraft();

            if (euAmount.HasValue && bgAmount.HasValue && (euAmount.Value + bgAmount.Value != bfpAmount.Value))
            {
                throw new DomainException("euAmount + bgAmount is not equal to bfpAmount in FinancialCorrectionVersion");
            }

            if (bfpAmount.HasValue && selfAmount.HasValue && (bfpAmount.Value + selfAmount.Value != totalAmount.Value))
            {
                throw new DomainException("bfpAmount + selfAmount is not equal to totalAmount in FinancialCorrectionVersion");
            }

            this.Percent = percent;
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.BfpAmount = bfpAmount;
            this.SelfAmount = selfAmount;
            this.TotalAmount = totalAmount;
            this.FinancialCorrectionImposingReasonId = financialCorrectionImposingReasonId;
            this.Description = description;
            this.ViolationFoundBy = violationFoundBy;
            this.AmendmentReason = amendmentReason;
            this.CorrectionBearer = correctionBearer;
            this.BlobKey = blobKey;

            var oldViolations = this.FinancialCorrectionVersionViolations.ToList();
            foreach (var violation in oldViolations)
            {
                this.FinancialCorrectionVersionViolations.Remove(violation);
            }

            foreach (var violationId in violationIds)
            {
                this.FinancialCorrectionVersionViolations.Add(new FinancialCorrectionVersionViolation()
                {
                    FinancialCorrectionVersionId = this.FinancialCorrectionVersionId,
                    OtherViolationId = violationId,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanChangeStatusToActual()
        {
            var errors = new List<string>();

            if (!this.TotalAmount.HasValue)
            {
                errors.Add("Полето 'Обща сума' трябва да е попълнено");
            }

            if (!this.FinancialCorrectionImposingReasonId.HasValue)
            {
                errors.Add("Полето 'Основание за налагане' трябва да е попълнено");
            }

            if (!this.CorrectionBearer.HasValue)
            {
                errors.Add("Полето 'Следва да се понесе от' трябва да е попълнено");
            }

            if (!this.ViolationFoundBy.HasValue)
            {
                errors.Add("Полето 'Орган/институция, установила нарушението' трябва да е попълнено");
            }

            if (!this.AmendmentReason.HasValue && this.OrderNum != 1)
            {
                errors.Add("Полето 'Причина за изменението' трябва да е попълнено");
            }

            return errors;
        }

        private void AssertIsDraft()
        {
            if (this.Status != FinancialCorrectionVersionStatus.Draft)
            {
                throw new DomainValidationException("FinancialCorrectionVersion must be in 'Draft' status.");
            }
        }
    }
}
