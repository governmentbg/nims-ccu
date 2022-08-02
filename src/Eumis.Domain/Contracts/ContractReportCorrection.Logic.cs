using Eumis.Domain.Core;
using System;
using System.Linq;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportCorrection
    {
        #region ContractReportCorrection
        public void UpdateData(
            Sign sign,
            DateTime date,
            string description,
            string reason,
            CorrectionTypeExtended? correctionType,
            int? financialCorrectionId,
            int? irregularityId,
            int? flatFinancialCorrectionId,
            decimal? correctedApprovedEuAmount,
            decimal? correctedApprovedBgAmount,
            decimal? correctedApprovedCrossAmount,
            decimal? correctedApprovedSelfAmount)
        {
            this.AssertIsDraft();

            this.UpdateCommonData(sign, date, description, reason);

            this.CorrectionType = correctionType;
            this.FinancialCorrectionId = financialCorrectionId;
            this.IrregularityId = irregularityId;
            this.FlatFinancialCorrectionId = flatFinancialCorrectionId;

            this.CorrectedApprovedEuAmount = correctedApprovedEuAmount;
            this.CorrectedApprovedBgAmount = correctedApprovedBgAmount;
            this.CorrectedApprovedCrossAmount = correctedApprovedCrossAmount;
            this.CorrectedApprovedSelfAmount = correctedApprovedSelfAmount;
            this.CorrectedApprovedBfpTotalAmount = correctedApprovedEuAmount.HasValue || correctedApprovedBgAmount.HasValue ?
                (correctedApprovedEuAmount ?? 0) + (correctedApprovedBgAmount ?? 0) :
                (decimal?)null;
            this.CorrectedApprovedTotalAmount = correctedApprovedEuAmount.HasValue || correctedApprovedBgAmount.HasValue || correctedApprovedSelfAmount.HasValue ?
                (correctedApprovedEuAmount ?? 0) + (correctedApprovedBgAmount ?? 0) + (correctedApprovedSelfAmount ?? 0) :
                (decimal?)null;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateCertAttributes(
            decimal? uncertifiedCorrectedApprovedEuAmount,
            decimal? uncertifiedCorrectedApprovedBgAmount,
            decimal? uncertifiedCorrectedApprovedBfpTotalAmount,
            decimal? uncertifiedCorrectedApprovedCrossAmount,
            decimal? uncertifiedCorrectedApprovedSelfAmount,
            decimal? uncertifiedCorrectedApprovedTotalAmount,
            decimal? certifiedCorrectedApprovedEuAmount,
            decimal? certifiedCorrectedApprovedBgAmount,
            decimal? certifiedCorrectedApprovedBfpTotalAmount,
            decimal? certifiedCorrectedApprovedCrossAmount,
            decimal? certifiedCorrectedApprovedSelfAmount,
            decimal? certifiedCorrectedApprovedTotalAmount)
        {
            this.ValidateTotalAmount(
                uncertifiedCorrectedApprovedEuAmount,
                uncertifiedCorrectedApprovedBgAmount,
                uncertifiedCorrectedApprovedBfpTotalAmount,
                uncertifiedCorrectedApprovedSelfAmount,
                uncertifiedCorrectedApprovedTotalAmount,
                "uncertifiedCorrectedApproved");
            this.ValidateTotalAmount(
                certifiedCorrectedApprovedEuAmount,
                certifiedCorrectedApprovedBgAmount,
                certifiedCorrectedApprovedBfpTotalAmount,
                certifiedCorrectedApprovedSelfAmount,
                certifiedCorrectedApprovedTotalAmount,
                "certifiedCorrectedApproved");

            this.UncertifiedCorrectedApprovedEuAmount = uncertifiedCorrectedApprovedEuAmount;
            this.UncertifiedCorrectedApprovedBgAmount = uncertifiedCorrectedApprovedBgAmount;
            this.UncertifiedCorrectedApprovedBfpTotalAmount = uncertifiedCorrectedApprovedBfpTotalAmount;
            this.UncertifiedCorrectedApprovedCrossAmount = uncertifiedCorrectedApprovedCrossAmount;
            this.UncertifiedCorrectedApprovedSelfAmount = uncertifiedCorrectedApprovedSelfAmount;
            this.UncertifiedCorrectedApprovedTotalAmount = uncertifiedCorrectedApprovedTotalAmount;

            this.CertifiedCorrectedApprovedEuAmount = certifiedCorrectedApprovedEuAmount;
            this.CertifiedCorrectedApprovedBgAmount = certifiedCorrectedApprovedBgAmount;
            this.CertifiedCorrectedApprovedBfpTotalAmount = certifiedCorrectedApprovedBfpTotalAmount;
            this.CertifiedCorrectedApprovedCrossAmount = certifiedCorrectedApprovedCrossAmount;
            this.CertifiedCorrectedApprovedSelfAmount = certifiedCorrectedApprovedSelfAmount;
            this.CertifiedCorrectedApprovedTotalAmount = certifiedCorrectedApprovedTotalAmount;

            this.ModifyDate = DateTime.Now;
        }

        private void ValidateTotalAmount(decimal? euAmount, decimal? bgAmount, decimal? bfpTotalAmount, decimal? selfAmount, decimal? totalAmount, string type)
        {
            if (euAmount.HasValue && bgAmount.HasValue)
            {
                if ((euAmount + bgAmount) != bfpTotalAmount)
                {
                    throw new DomainException("ContractReportCorrection total " + type + " amount is not correct!");
                }

                if (selfAmount.HasValue && ((euAmount + bgAmount + selfAmount) != totalAmount))
                {
                    throw new DomainException("ContractReportCorrection total " + type + " amount is not correct!");
                }
            }
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = ContractReportCorrectionStatus.Draft;
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
            this.Status = ContractReportCorrectionStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDeleted(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("CompensationDocument must be activated!");
            }

            this.Status = ContractReportCorrectionStatus.Deleted;
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
            if (this.Status != ContractReportCorrectionStatus.Draft)
            {
                throw new DomainValidationException("ContractReportCorrection status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != ContractReportCorrectionStatus.Entered)
            {
                throw new DomainValidationException("ContractReportCorrection status must be entered!");
            }
        }
        #endregion //ContractReportCorrection

        #region ContractReportCorrectionDocument
        public void AddDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            this.Documents.Add(new ContractReportCorrectionDocument()
            {
                ContractReportCorrectionId = this.ContractReportCorrectionId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public ContractReportCorrectionDocument GetDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.ContractReportCorrectionDocumentId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ContractReportCorrectionDocument with id " + documentId);
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
        #endregion //ContractReportCorrectionDocument
    }
}
