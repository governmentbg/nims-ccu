using System;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public partial class Prognosis
    {
        public void UpdateData(
            decimal? contractedEuAmount,
            decimal? contractedBgAmount,
            decimal? paymentEuAmount,
            decimal? paymentBgAmount,
            decimal? advancePaymentEuAmount,
            decimal? advancePaymentBgAmount,
            decimal? advanceVerPaymentEuAmount,
            decimal? advanceVerPaymentBgAmount,
            decimal? intermediatePaymentEuAmount,
            decimal? intermediatePaymentBgAmount,
            decimal? finalPaymentEuAmount,
            decimal? finalPaymentBgAmount,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? certifiedEuAmount,
            decimal? certifiedBgAmount)
        {
            this.AssertIsDraft();

            this.Contracted.SetAttributes(contractedEuAmount, contractedBgAmount);
            this.Payment.SetAttributes(paymentEuAmount, paymentBgAmount);
            this.AdvancePayment.SetAttributes(advancePaymentEuAmount, advancePaymentBgAmount);
            this.AdvanceVerPayment.SetAttributes(advanceVerPaymentEuAmount, advanceVerPaymentBgAmount);
            this.IntermediatePayment.SetAttributes(intermediatePaymentEuAmount, intermediatePaymentBgAmount);
            this.FinalPayment.SetAttributes(finalPaymentEuAmount, finalPaymentBgAmount);
            this.Approved.SetAttributes(approvedEuAmount, approvedBgAmount);
            this.Certified.SetAttributes(certifiedEuAmount, certifiedBgAmount);

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = PrognosisStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered()
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                this.IsActivated = true;
            }

            this.Status = PrognosisStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDeleted(string deleteNote)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("Prognosis must be activated!");
            }

            this.Status = PrognosisStatus.Deleted;
            this.DeleteNote = deleteNote;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != PrognosisStatus.Draft)
            {
                throw new DomainValidationException("Prognosis status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != PrognosisStatus.Entered)
            {
                throw new DomainValidationException("Prognosis status must be entered!");
            }
        }
    }
}
