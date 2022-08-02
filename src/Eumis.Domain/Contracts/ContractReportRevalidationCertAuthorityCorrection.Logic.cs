using Eumis.Domain.Core;
using System;
using System.Linq;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportRevalidationCertAuthorityCorrection
    {
        #region ContractReportRevalidationCertAuthorityCorrection

        public void UpdateData(
            Sign sign,
            DateTime date,
            string description,
            string reason,
            decimal? certifiedRevalidatedEuAmount,
            decimal? certifiedRevalidatedBgAmount,
            decimal? certifiedRevalidatedCrossAmount,
            decimal? certifiedRevalidatedSelfAmount)
        {
            this.AssertIsDraft();

            this.UpdateCommonData(sign, date, description, reason);

            this.CertifiedRevalidatedEuAmount = certifiedRevalidatedEuAmount;
            this.CertifiedRevalidatedBgAmount = certifiedRevalidatedBgAmount;
            this.CertifiedRevalidatedCrossAmount = certifiedRevalidatedCrossAmount;
            this.CertifiedRevalidatedSelfAmount = certifiedRevalidatedSelfAmount;
            this.CertifiedRevalidatedBfpTotalAmount = certifiedRevalidatedEuAmount.HasValue || certifiedRevalidatedBgAmount.HasValue ?
                (certifiedRevalidatedEuAmount ?? 0) + (certifiedRevalidatedBgAmount ?? 0) :
                (decimal?)null;
            this.CertifiedRevalidatedTotalAmount = certifiedRevalidatedEuAmount.HasValue || certifiedRevalidatedBgAmount.HasValue || certifiedRevalidatedSelfAmount.HasValue ?
                (certifiedRevalidatedEuAmount ?? 0) + (certifiedRevalidatedBgAmount ?? 0) + (certifiedRevalidatedSelfAmount ?? 0) :
                (decimal?)null;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = ContractReportRevalidationCertAuthorityCorrectionStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered(int userId, string regNumber = null)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                this.IsActivated = true;
                this.RegNumber = regNumber;
            }

            this.CheckedDate = DateTime.Now;
            this.CheckedByUserId = userId;
            this.Status = ContractReportRevalidationCertAuthorityCorrectionStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDeleted(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("ContractReportRevalidationCertAuthorityCorrection must be activated!");
            }

            this.Status = ContractReportRevalidationCertAuthorityCorrectionStatus.Deleted;
            this.DeleteNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        private void UpdateCommonData(
            Sign sign,
            DateTime date,
            string description,
            string reason)
        {
            this.Sign = sign;
            this.Date = date;
            this.Description = description;
            this.Reason = reason;
        }

        private void AssertIsDraft()
        {
            if (this.Status != ContractReportRevalidationCertAuthorityCorrectionStatus.Draft)
            {
                throw new DomainValidationException("ContractReportRevalidationCertAuthorityCorrection status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != ContractReportRevalidationCertAuthorityCorrectionStatus.Entered)
            {
                throw new DomainValidationException("ContractReportRevalidationCertAuthorityCorrection status must be entered!");
            }
        }

        #endregion ContractReportRevalidationCertAuthorityCorrection

        #region ContractReportRevalidationCertAuthorityCorrectionDocument

        public void AddDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            this.Documents.Add(new ContractReportRevalidationCertAuthorityCorrectionDocument()
            {
                ContractReportRevalidationCertAuthorityCorrectionId = this.ContractReportRevalidationCertAuthorityCorrectionId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public ContractReportRevalidationCertAuthorityCorrectionDocument GetDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.ContractReportRevalidationCertAuthorityCorrectionDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ContractReportRevalidationCertAuthorityCorrectionDocument with id " + documentId);
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

        #endregion ContractReportRevalidationCertAuthorityCorrectionDocument
    }
}
