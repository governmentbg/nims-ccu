using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.EuReimbursedAmounts
{
    public partial class EuReimbursedAmount
    {
        #region EuReimbursedAmount

        public void UpdateData(
            EuReimbursedAmountPaymentType? paymentType,
            DateTime? date,
            string paymentAppNum,
            DateTime? paymentAppSentDate,
            DateTime? paymentAppDateFrom,
            DateTime? paymentAppDateTo,
            decimal? certExpensesBfpEuAmountLv,
            decimal? certExpensesBfpBgAmountLv,
            decimal? certExpensesSelfAmountLv,
            decimal? certExpensesBfpEuAmountEuro,
            decimal? certExpensesBfpBgAmountEuro,
            decimal? certExpensesSelfAmountEuro,
            decimal? euTranche,
            string note)
        {
            this.AssertIsDraft();

            this.PaymentType = paymentType;
            this.Date = date;

            this.PaymentAppNum = paymentAppNum;
            this.PaymentAppSentDate = paymentAppSentDate;
            this.PaymentAppDateFrom = paymentAppDateFrom;
            this.PaymentAppDateTo = paymentAppDateTo;

            this.CertExpensesLv.SetAttributes(certExpensesBfpEuAmountLv, certExpensesBfpBgAmountLv, certExpensesSelfAmountLv);
            this.CertExpensesEuro.SetAttributes(certExpensesBfpEuAmountEuro, certExpensesBfpBgAmountEuro, certExpensesSelfAmountEuro);

            this.EuTranche = euTranche;
            this.Note = note;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered()
        {
            this.AssertIsDraft();

            if (this.CanChangeStatusToEntered().Count != 0)
            {
                throw new DomainException("Cannot change status to entered of EuReimbursedAmount");
            }

            if (!this.IsActivated)
            {
                this.IsActivated = true;
            }

            this.Status = EuReimbursedAmountStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanChangeStatusToEntered()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(this.PaymentAppNum))
            {
                errors.Add("Полето 'Пореден номер' трябва да е попълнено.");
            }

            if (!this.Date.HasValue)
            {
                errors.Add("Полето 'Дата' трябва да е попълнено.");
            }

            return errors;
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = EuReimbursedAmountStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToRemoved(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("EuReimbursedAmount must be activated!");
            }

            this.Status = EuReimbursedAmountStatus.Removed;
            this.DeleteNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != EuReimbursedAmountStatus.Draft)
            {
                throw new DomainValidationException("EuReimbursedAmount status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != EuReimbursedAmountStatus.Entered)
            {
                throw new DomainValidationException("EuReimbursedAmount status must be entered!");
            }
        }

        #endregion //EuReimbursedAmount

        #region EuReimbursedAmountCertReport
        public void AddCertReport(int certReportId)
        {
            this.AssertIsDraft();

            this.CertReports.Add(new EuReimbursedAmountCertReport
                {
                    EuReimbursedAmountId = this.EuReimbursedAmountId,
                    CertReportId = certReportId,
                });

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveCertReport(int itemId)
        {
            this.AssertIsDraft();

            var certReport = this.CertReports.Single(li => li.EuReimbursedAmountCertReportId == itemId);
            this.CertReports.Remove(certReport);

            this.ModifyDate = DateTime.Now;
        }
        #endregion //EuReimbursedAmountCertReport
    }
}
