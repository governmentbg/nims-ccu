using System;
using Eumis.Domain.NonAggregates;
using System.Linq;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public partial class ActuallyPaidAmount
    {
        public void UpdateData(
            int programmePriorityId,
            PaymentReason paymentReason,
            DateTime? paymentDate,
            string comment,
            decimal? paidBfpEuAmount,
            decimal? paidBfpBgAmount,
            decimal? paidSelfAmount,
            decimal? paidBfpCrossAmount)
        {
            this.AssertIsDraft();

            if (this.SapFileId != null)
            {
                throw new DomainValidationException($"ActuallyPaidAmount created from a SapFile cannot be updated with this method. Use {nameof(this.UpdateSapData)} method instead!");
            }

            this.ProgrammePriorityId = programmePriorityId;

            this.PaymentReason = paymentReason;
            this.PaymentDate = paymentDate;
            this.Comment = comment;

            this.PaidBfpEuAmount = paidBfpEuAmount;
            this.PaidBfpBgAmount = paidBfpBgAmount;
            this.UpdateBfpTotalAmount();

            this.PaidSelfAmount = paidSelfAmount;
            this.UpdateTotalAmount();

            this.PaidBfpCrossAmount = paidBfpCrossAmount;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateSapData(
            int programmePriorityId,
            PaymentReason paymentReason,
            string comment,
            decimal? paidSelfAmount,
            decimal? paidBfpCrossAmount)
        {
            this.AssertIsDraft();

            if (this.SapFileId == null)
            {
                throw new DomainValidationException($"ActuallyPaidAmount not created from a SapFile cannot be updated with this method. Use {nameof(this.UpdateData)} method instead!");
            }

            this.ProgrammePriorityId = programmePriorityId;

            this.PaymentReason = paymentReason;
            this.Comment = comment;

            this.PaidSelfAmount = paidSelfAmount;
            this.UpdateTotalAmount();

            this.PaidBfpCrossAmount = paidBfpCrossAmount;

            this.ModifyDate = DateTime.Now;
        }

        public void AssignContractReportPayment(int contractReportPaymentId)
        {
            if (this.ContractReportPaymentId != null)
            {
                throw new DomainValidationException("Cannot assign ContractReportPayment. Use ChangeContractReportPayment instead.");
            }

            this.AssertIsDraft();

            this.ContractReportPaymentId = contractReportPaymentId;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeContractReportPayment(int contractReportPaymentId)
        {
            if (this.ContractReportPaymentId == null)
            {
                throw new DomainValidationException("Cannot change ContractReportPayment. Use AssignContractReportPayment instead.");
            }

            this.AssertIsDraft();

            this.ContractReportPaymentId = contractReportPaymentId;

            this.ModifyDate = DateTime.Now;
        }

        public void DissociateContractReportPayment()
        {
            if (this.ContractReportPaymentId == null)
            {
                throw new DomainValidationException("Cannot dissociate ContractReportPayment when none has been assigned.");
            }

            this.AssertIsDraft();

            this.ContractReportPaymentId = null;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = ActuallyPaidAmountStatus.Draft;
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

            this.Status = ActuallyPaidAmountStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDeleted(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("ActuallyPaidAmount must be activated!");
            }

            this.Status = ActuallyPaidAmountStatus.Deleted;
            this.IsDeletedNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != ActuallyPaidAmountStatus.Draft)
            {
                throw new DomainValidationException("ActuallyPaidAmount status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != ActuallyPaidAmountStatus.Entered)
            {
                throw new DomainValidationException("ActuallyPaidAmount status must be entered!");
            }
        }

        private void UpdateBfpTotalAmount()
        {
            this.PaidBfpTotalAmount = this.PaidBfpEuAmount.HasValue || this.PaidBfpBgAmount.HasValue ?
                (this.PaidBfpEuAmount ?? 0) + (this.PaidBfpBgAmount ?? 0) :
                (decimal?)null;
        }

        private void UpdateTotalAmount()
        {
            this.PaidTotalAmount = this.PaidBfpEuAmount.HasValue || this.PaidBfpBgAmount.HasValue || this.PaidSelfAmount.HasValue ?
                (this.PaidBfpEuAmount ?? 0) + (this.PaidBfpBgAmount ?? 0) + (this.PaidSelfAmount ?? 0) :
                (decimal?)null;
        }

        #region ActuallyPaidAmountDocument

        public ActuallyPaidAmountDocument FindActuallyPaidAmountDocument(int documentId)
        {
            var document = this.ActuallyPaidAmountDocuments.Where(d => d.ActuallyPaidAmountDocumentId == documentId).SingleOrDefault();

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ActuallyPaidAmountDocument with documentId " + documentId);
            }

            return document;
        }

        public void UpdateActuallyPaidAmountDocument(
            int documentId,
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsDraft();

            var document = this.FindActuallyPaidAmountDocument(documentId);

            document.SetAttributes(name, description, blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public void AddActuallyPaidAmountDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsDraft();

            this.ActuallyPaidAmountDocuments.Add(new ActuallyPaidAmountDocument()
            {
                ActuallyPaidAmountId = this.ActuallyPaidAmountId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveActuallyPaidAmountDocument(int documentId)
        {
            this.AssertIsDraft();

            var document = this.FindActuallyPaidAmountDocument(documentId);

            this.ActuallyPaidAmountDocuments.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //ActuallyPaidAmountDocument
    }
}
