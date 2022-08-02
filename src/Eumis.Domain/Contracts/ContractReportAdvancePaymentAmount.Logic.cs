using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportAdvancePaymentAmount
    {
        public void UpdateAttributes(
            ContractReportAdvancePaymentAmountApproval? approval,
            string notes,
            decimal? approvedEuAmount,
            decimal? approvedBgAmount,
            decimal? approvedBfpTotalAmount)
        {
            this.Approval = approval;
            this.Notes = notes;

            this.ApprovedEuAmount = approvedEuAmount;
            this.ApprovedBgAmount = approvedBgAmount;
            this.ApprovedBfpTotalAmount = approvedBfpTotalAmount;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateCertAttributes(
            decimal? unCertifiedApprovedEuAmount,
            decimal? unCertifiedApprovedBgAmount,
            decimal? unCertifiedApprovedBfpTotalAmount,
            decimal? certifiedApprovedEuAmount,
            decimal? certifiedApprovedBgAmount,
            decimal? certifiedApprovedBfpTotalAmount)
        {
            this.UncertifiedApprovedEuAmount = unCertifiedApprovedEuAmount;
            this.UncertifiedApprovedBgAmount = unCertifiedApprovedBgAmount;
            this.UncertifiedApprovedBfpTotalAmount = unCertifiedApprovedBfpTotalAmount;

            this.CertifiedApprovedEuAmount = certifiedApprovedEuAmount;
            this.CertifiedApprovedBgAmount = certifiedApprovedBgAmount;
            this.CertifiedApprovedBfpTotalAmount = certifiedApprovedBfpTotalAmount;

            this.ModifyDate = DateTime.Now;
        }
    }
}
