using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancialCSDBudgetItem
    {
        public void UpdateAttributes(
            bool? costSupportingDocumentApproved,
            string notes,
            decimal euAmount,
            decimal bgAmount,
            decimal? unapprovedEuAmount,
            decimal? unapprovedBgAmount,
            decimal? unapprovedBfpTotalAmount,
            decimal? unapprovedSelfAmount,
            decimal? unapprovedTotalAmount,
            decimal? unapprovedByCorrectionEuAmount,
            decimal? unapprovedByCorrectionBgAmount,
            decimal? unapprovedByCorrectionBfpTotalAmount,
            decimal? unapprovedByCorrectionSelfAmount,
            decimal? unapprovedByCorrectionTotalAmount,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? approvedBfpTotalAmount,
            decimal? approvedSelfAmount,
            decimal? approvedTotalAmount,
            CorrectionType? correctionType,
            int? financialCorrectionId,
            int? irregularityId)
        {
            this.CostSupportingDocumentApproved = costSupportingDocumentApproved;
            this.Notes = notes;
            this.EuAmount = euAmount;
            this.BgAmount = bgAmount;
            this.UnapprovedEuAmount = unapprovedEuAmount;
            this.UnapprovedBgAmount = unapprovedBgAmount;
            this.UnapprovedBfpTotalAmount = unapprovedBfpTotalAmount;
            this.UnapprovedSelfAmount = unapprovedSelfAmount;
            this.UnapprovedTotalAmount = unapprovedTotalAmount;
            this.UnapprovedByCorrectionEuAmount = unapprovedByCorrectionEuAmount;
            this.UnapprovedByCorrectionBgAmount = unapprovedByCorrectionBgAmount;
            this.UnapprovedByCorrectionBfpTotalAmount = unapprovedByCorrectionBfpTotalAmount;
            this.UnapprovedByCorrectionSelfAmount = unapprovedByCorrectionSelfAmount;
            this.UnapprovedByCorrectionTotalAmount = unapprovedByCorrectionTotalAmount;
            this.ApprovedEuAmount = approvedEuAmount;
            this.ApprovedBgAmount = approvedBgAmount;
            this.ApprovedBfpTotalAmount = approvedBfpTotalAmount;
            this.ApprovedSelfAmount = approvedSelfAmount;
            this.ApprovedTotalAmount = approvedTotalAmount;

            this.CorrectionType = correctionType;
            this.FinancialCorrectionId = financialCorrectionId;
            this.IrregularityId = irregularityId;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateCertAttributes(
            decimal? uncertifiedApprovedEuAmount,
            decimal? uncertifiedApprovedBgAmount,
            decimal? uncertifiedApprovedBfpTotalAmount,
            decimal? uncertifiedApprovedSelfAmount,
            decimal? uncertifiedApprovedTotalAmount,
            decimal? certifiedApprovedEuAmount,
            decimal? certifiedApprovedBgAmount,
            decimal? certifiedApprovedBfpTotalAmount,
            decimal? certifiedApprovedSelfAmount,
            decimal? certifiedApprovedTotalAmount)
        {
            this.UncertifiedApprovedEuAmount = uncertifiedApprovedEuAmount;
            this.UncertifiedApprovedBgAmount = uncertifiedApprovedBgAmount;
            this.UncertifiedApprovedBfpTotalAmount = uncertifiedApprovedBfpTotalAmount;
            this.UncertifiedApprovedSelfAmount = uncertifiedApprovedSelfAmount;
            this.UncertifiedApprovedTotalAmount = uncertifiedApprovedTotalAmount;
            this.CertifiedApprovedEuAmount = certifiedApprovedEuAmount;
            this.CertifiedApprovedBgAmount = certifiedApprovedBgAmount;
            this.CertifiedApprovedBfpTotalAmount = certifiedApprovedBfpTotalAmount;
            this.CertifiedApprovedSelfAmount = certifiedApprovedSelfAmount;
            this.CertifiedApprovedTotalAmount = certifiedApprovedTotalAmount;

            this.ModifyDate = DateTime.Now;
        }
    }
}
