using Eumis.Domain.Core;
using System;
using System.Linq;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportCertAuthorityCorrection
    {
        #region ContractReportCertAuthorityCorrection
        public void UpdateData(
            Sign sign,
            DateTime date,
            string description,
            string reason,
            decimal? certifiedEuAmount,
            decimal? certifiedBgAmount,
            decimal? certifiedCrossAmount,
            decimal? certifiedSelfAmount)
        {
            this.AssertIsDraft();

            this.UpdateCommonData(sign, date, description, reason);

            this.CertifiedEuAmount = certifiedEuAmount;
            this.CertifiedBgAmount = certifiedBgAmount;
            this.CertifiedCrossAmount = certifiedCrossAmount;
            this.CertifiedSelfAmount = certifiedSelfAmount;
            this.CertifiedBfpTotalAmount = certifiedEuAmount.HasValue || certifiedBgAmount.HasValue ?
                (certifiedEuAmount ?? 0) + (certifiedBgAmount ?? 0) :
                (decimal?)null;
            this.CertifiedTotalAmount = certifiedEuAmount.HasValue || certifiedBgAmount.HasValue || certifiedSelfAmount.HasValue ?
                (certifiedEuAmount ?? 0) + (certifiedBgAmount ?? 0) + (certifiedSelfAmount ?? 0) :
                (decimal?)null;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = ContractReportCertAuthorityCorrectionStatus.Draft;
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
            this.Status = ContractReportCertAuthorityCorrectionStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDeleted(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("CompensationDocument must be activated!");
            }

            this.Status = ContractReportCertAuthorityCorrectionStatus.Deleted;
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
            if (this.Status != ContractReportCertAuthorityCorrectionStatus.Draft)
            {
                throw new DomainValidationException("ContractReportCertAuthorityCorrection status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != ContractReportCertAuthorityCorrectionStatus.Entered)
            {
                throw new DomainValidationException("ContractReportCertAuthorityCorrection status must be entered!");
            }
        }
        #endregion //ContractReportCertAuthorityCorrection

        #region ContractReportCertAuthorityCorrectionDocument
        public void AddDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            this.Documents.Add(new ContractReportCertAuthorityCorrectionDocument()
            {
                ContractReportCertAuthorityCorrectionId = this.ContractReportCertAuthorityCorrectionId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public ContractReportCertAuthorityCorrectionDocument GetDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.ContractReportCertAuthorityCorrectionDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ContractReportCertAuthorityCorrectionDocument with id " + documentId);
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
        #endregion //ContractReportCertAuthorityCorrectionDocument
    }
}
