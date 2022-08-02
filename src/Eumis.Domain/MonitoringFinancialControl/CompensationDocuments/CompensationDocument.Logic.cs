using System;
using System.Linq;

namespace Eumis.Domain.MonitoringFinancialControl.CompensationDocuments
{
    public partial class CompensationDocument
    {
        #region CompensationDocument
        public void UpdateData(
            CompensationSign compensationSign,
            DateTime compensationDocDate,
            string description,
            string compensationReason,
            decimal? bfpEuAmount,
            decimal? bfpBgAmount,
            decimal? bfpCrossAmount,
            decimal? selfAmount)
        {
            this.AssertIsDraft();

            this.UpdateCommonData(compensationSign, compensationDocDate, description, compensationReason);

            this.BfpEuAmount = bfpEuAmount;
            this.BfpBgAmount = bfpBgAmount;

            if (this.Type == CompensationDocumentType.ActuallyPaidAmount)
            {
                this.BfpCrossAmount = bfpCrossAmount;
                this.BfpTotalAmount = bfpEuAmount.HasValue || bfpBgAmount.HasValue ?
                    (bfpEuAmount ?? 0) + (bfpBgAmount ?? 0) :
                    (decimal?)null;
            }
            else
            {
                this.SelfAmount = selfAmount;
                this.BfpTotalAmount = bfpEuAmount.HasValue || bfpBgAmount.HasValue ?
                    (bfpEuAmount ?? 0) + (bfpBgAmount ?? 0) :
                    (decimal?)null;
                this.TotalAmount = bfpEuAmount.HasValue || bfpBgAmount.HasValue || selfAmount.HasValue ?
                    (bfpEuAmount ?? 0) + (bfpBgAmount ?? 0) + (selfAmount ?? 0) :
                    (decimal?)null;
            }

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = CompensationDocumentStatus.Draft;
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

            this.Status = CompensationDocumentStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDeleted(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("CompensationDocument must be activated!");
            }

            this.Status = CompensationDocumentStatus.Deleted;
            this.DeleteNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        private void UpdateCommonData(
            CompensationSign compensationSign,
            DateTime compensationDocDate,
            string description,
            string compensationReason)
        {
            this.CompensationSign = compensationSign;
            this.CompensationDocDate = compensationDocDate;
            this.Description = description;
            this.CompensationReason = compensationReason;
        }

        private void AssertIsDraft()
        {
            if (this.Status != CompensationDocumentStatus.Draft)
            {
                throw new DomainValidationException("CompensationDocument status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != CompensationDocumentStatus.Entered)
            {
                throw new DomainValidationException("CompensationDocument status must be entered!");
            }
        }
        #endregion //CompensationDocument

        #region CompensationDocumentDoc
        public void AddDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            this.Documents.Add(new CompensationDocumentDoc()
            {
                CompensationDocumentId = this.CompensationDocumentId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public CompensationDocumentDoc GetDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.CompensationDocumentDocId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find CompensationDocumentDoc with id " + documentId);
            }

            return document;
        }

        public void UpdateDocument(
            int documentId,
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            var document = this.GetDocument(documentId);
            document.SetAttributes(description, fileName, fileKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveDocument(int documentId)
        {
            this.AssertIsDraft();

            var document = this.GetDocument(documentId);
            this.Documents.Remove(document);

            this.ModifyDate = DateTime.Now;
        }
        #endregion //CompensationDocumentDoc
    }
}
