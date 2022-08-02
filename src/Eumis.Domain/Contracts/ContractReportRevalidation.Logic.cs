using System;
using System.Linq;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportRevalidation
    {
        #region ContractReportRevalidation
        public void UpdateData(
            DateTime date,
            string description,
            string reason,
            decimal? revalidatedEuAmount,
            decimal? revalidatedBgAmount,
            decimal? revalidatedCrossAmount,
            decimal? revalidatedSelfAmount)
        {
            this.AssertIsDraft();

            this.UpdateCommonData(date, description, reason);

            this.RevalidatedEuAmount = revalidatedEuAmount;
            this.RevalidatedBgAmount = revalidatedBgAmount;
            this.RevalidatedCrossAmount = revalidatedCrossAmount;
            this.RevalidatedSelfAmount = revalidatedSelfAmount;
            this.RevalidatedBfpTotalAmount = revalidatedEuAmount.HasValue || revalidatedBgAmount.HasValue ?
                (revalidatedEuAmount ?? 0) + (revalidatedBgAmount ?? 0) :
                (decimal?)null;
            this.RevalidatedTotalAmount = revalidatedEuAmount.HasValue || revalidatedBgAmount.HasValue || revalidatedSelfAmount.HasValue ?
                (revalidatedEuAmount ?? 0) + (revalidatedBgAmount ?? 0) + (revalidatedSelfAmount ?? 0) :
                (decimal?)null;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateCertAttributes(
            decimal? uncertifiedRevalidatedEuAmount,
            decimal? uncertifiedRevalidatedBgAmount,
            decimal? uncertifiedRevalidatedBfpTotalAmount,
            decimal? uncertifiedRevalidatedCrossAmount,
            decimal? uncertifiedRevalidatedSelfAmount,
            decimal? uncertifiedRevalidatedTotalAmount,
            decimal? certifiedRevalidatedEuAmount,
            decimal? certifiedRevalidatedBgAmount,
            decimal? certifiedRevalidatedBfpTotalAmount,
            decimal? certifiedRevalidatedCrossAmount,
            decimal? certifiedRevalidatedSelfAmount,
            decimal? certifiedRevalidatedTotalAmount)
        {
            this.ValidateTotalAmount(
                uncertifiedRevalidatedEuAmount,
                uncertifiedRevalidatedBgAmount,
                uncertifiedRevalidatedBfpTotalAmount,
                uncertifiedRevalidatedSelfAmount,
                uncertifiedRevalidatedTotalAmount,
                "uncertifiedRevalidated");
            this.ValidateTotalAmount(
                certifiedRevalidatedEuAmount,
                certifiedRevalidatedBgAmount,
                certifiedRevalidatedBfpTotalAmount,
                certifiedRevalidatedSelfAmount,
                certifiedRevalidatedTotalAmount,
                "certifiedRevalidated");

            this.UncertifiedRevalidatedEuAmount = uncertifiedRevalidatedEuAmount;
            this.UncertifiedRevalidatedBgAmount = uncertifiedRevalidatedBgAmount;
            this.UncertifiedRevalidatedBfpTotalAmount = uncertifiedRevalidatedBfpTotalAmount;
            this.UncertifiedRevalidatedCrossAmount = uncertifiedRevalidatedCrossAmount;
            this.UncertifiedRevalidatedSelfAmount = uncertifiedRevalidatedSelfAmount;
            this.UncertifiedRevalidatedTotalAmount = uncertifiedRevalidatedTotalAmount;

            this.CertifiedRevalidatedEuAmount = certifiedRevalidatedEuAmount;
            this.CertifiedRevalidatedBgAmount = certifiedRevalidatedBgAmount;
            this.CertifiedRevalidatedBfpTotalAmount = certifiedRevalidatedBfpTotalAmount;
            this.CertifiedRevalidatedCrossAmount = certifiedRevalidatedCrossAmount;
            this.CertifiedRevalidatedSelfAmount = certifiedRevalidatedSelfAmount;
            this.CertifiedRevalidatedTotalAmount = certifiedRevalidatedTotalAmount;

            this.ModifyDate = DateTime.Now;
        }

        private void ValidateTotalAmount(decimal? euAmount, decimal? bgAmount, decimal? bfpTotalAmount, decimal? selfAmount, decimal? totalAmount, string type)
        {
            if (euAmount.HasValue && bgAmount.HasValue)
            {
                if ((euAmount + bgAmount) != bfpTotalAmount)
                {
                    throw new DomainException("ContractReportRevalidation total " + type + " amount is not correct!");
                }

                if (selfAmount.HasValue && ((euAmount + bgAmount + selfAmount) != totalAmount))
                {
                    throw new DomainException("ContractReportRevalidation total " + type + " amount is not correct!");
                }
            }
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = ContractReportRevalidationStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered(int? userId, string regNumber = null)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                this.IsActivated = true;
                this.RegNumber = regNumber;
            }

            this.CheckedDate = DateTime.Now;
            this.CheckedByUserId = userId;
            this.Status = ContractReportRevalidationStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDeleted(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("CompensationDocument must be activated!");
            }

            this.Status = ContractReportRevalidationStatus.Deleted;
            this.DeleteNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        private void UpdateCommonData(
            DateTime date,
            string description,
            string reason)
        {
            this.Date = date;
            this.Description = description;
            this.Reason = reason;
        }

        private void AssertIsDraft()
        {
            if (this.Status != ContractReportRevalidationStatus.Draft)
            {
                throw new DomainValidationException("ContractReportRevalidation status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != ContractReportRevalidationStatus.Entered)
            {
                throw new DomainValidationException("ContractReportRevalidation status must be entered!");
            }
        }
        #endregion //ContractReportRevalidation

        #region ContractReportRevalidationDocument
        public void AddDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            this.Documents.Add(new ContractReportRevalidationDocument()
            {
                ContractReportRevalidationId = this.ContractReportRevalidationId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public ContractReportRevalidationDocument GetDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.ContractReportRevalidationDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ContractReportRevalidationDocument with id " + documentId);
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
        #endregion //ContractReportRevalidationDocument
    }
}
