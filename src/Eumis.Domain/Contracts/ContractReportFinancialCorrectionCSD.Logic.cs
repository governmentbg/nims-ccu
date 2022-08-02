using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancialCorrectionCSD
    {
        public void UpdateAttributes(
            Sign? sign,
            string notes,
            decimal? correctedUnapprovedEuAmount,
            decimal? correctedUnapprovedBgAmount,
            decimal? correctedUnapprovedBfpTotalAmount,
            decimal? correctedUnapprovedSelfAmount,
            decimal? correctedUnapprovedTotalAmount,
            decimal? correctedUnapprovedByCorrectionEuAmount,
            decimal? correctedUnapprovedByCorrectionBgAmount,
            decimal? correctedUnapprovedByCorrectionBfpTotalAmount,
            decimal? correctedUnapprovedByCorrectionSelfAmount,
            decimal? correctedUnapprovedByCorrectionTotalAmount,
            decimal? correctedApprovedEuAmount,
            decimal? correctedApprovedBgAmount,
            decimal? correctedApprovedBfpTotalAmount,
            decimal? correctedApprovedSelfAmount,
            decimal? correctedApprovedTotalAmount,
            CorrectionType? correctionType,
            int? financialCorrectionId,
            int? irregularityId)
        {
            this.ValidateTotalAmount(
                correctedUnapprovedEuAmount,
                correctedUnapprovedBgAmount,
                correctedUnapprovedBfpTotalAmount,
                correctedUnapprovedSelfAmount,
                correctedUnapprovedTotalAmount,
                "correctedUnapproved");
            this.ValidateTotalAmount(
                correctedUnapprovedByCorrectionEuAmount,
                correctedUnapprovedByCorrectionBgAmount,
                correctedUnapprovedByCorrectionBfpTotalAmount,
                correctedUnapprovedByCorrectionSelfAmount,
                correctedUnapprovedByCorrectionTotalAmount,
                "correctedUnapprovedByCorrection");
            this.ValidateTotalAmount(
                correctedApprovedEuAmount,
                correctedApprovedBgAmount,
                correctedApprovedBfpTotalAmount,
                correctedApprovedSelfAmount,
                correctedApprovedTotalAmount,
                "correctedApproved");

            this.Sign = sign;
            this.Notes = notes;
            this.CorrectedUnapprovedEuAmount = correctedUnapprovedEuAmount;
            this.CorrectedUnapprovedBgAmount = correctedUnapprovedBgAmount;
            this.CorrectedUnapprovedBfpTotalAmount = correctedUnapprovedBfpTotalAmount;
            this.CorrectedUnapprovedSelfAmount = correctedUnapprovedSelfAmount;
            this.CorrectedUnapprovedTotalAmount = correctedUnapprovedTotalAmount;
            this.CorrectedUnapprovedByCorrectionEuAmount = correctedUnapprovedByCorrectionEuAmount;
            this.CorrectedUnapprovedByCorrectionBgAmount = correctedUnapprovedByCorrectionBgAmount;
            this.CorrectedUnapprovedByCorrectionBfpTotalAmount = correctedUnapprovedByCorrectionBfpTotalAmount;
            this.CorrectedUnapprovedByCorrectionSelfAmount = correctedUnapprovedByCorrectionSelfAmount;
            this.CorrectedUnapprovedByCorrectionTotalAmount = correctedUnapprovedByCorrectionTotalAmount;
            this.CorrectedApprovedEuAmount = correctedApprovedEuAmount;
            this.CorrectedApprovedBgAmount = correctedApprovedBgAmount;
            this.CorrectedApprovedBfpTotalAmount = correctedApprovedBfpTotalAmount;
            this.CorrectedApprovedSelfAmount = correctedApprovedSelfAmount;
            this.CorrectedApprovedTotalAmount = correctedApprovedTotalAmount;

            this.CorrectionType = correctionType;
            this.FinancialCorrectionId = financialCorrectionId;
            this.IrregularityId = irregularityId;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateCertAttributes(
            decimal? uncertifiedCorrectedApprovedEuAmount,
            decimal? uncertifiedCorrectedApprovedBgAmount,
            decimal? uncertifiedCorrectedApprovedBfpTotalAmount,
            decimal? uncertifiedCorrectedApprovedSelfAmount,
            decimal? uncertifiedCorrectedApprovedTotalAmount,
            decimal? certifiedCorrectedApprovedEuAmount,
            decimal? certifiedCorrectedApprovedBgAmount,
            decimal? certifiedCorrectedApprovedBfpTotalAmount,
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
            this.UncertifiedCorrectedApprovedSelfAmount = uncertifiedCorrectedApprovedSelfAmount;
            this.UncertifiedCorrectedApprovedTotalAmount = uncertifiedCorrectedApprovedTotalAmount;

            this.CertifiedCorrectedApprovedEuAmount = certifiedCorrectedApprovedEuAmount;
            this.CertifiedCorrectedApprovedBgAmount = certifiedCorrectedApprovedBgAmount;
            this.CertifiedCorrectedApprovedBfpTotalAmount = certifiedCorrectedApprovedBfpTotalAmount;
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
                    throw new DomainException("ContractReportFinancialCorrectionCSD total " + type + " amount is not correct!");
                }

                if (selfAmount.HasValue && ((euAmount + bgAmount + selfAmount) != totalAmount))
                {
                    throw new DomainException("ContractReportFinancialCorrectionCSD total " + type + " amount is not correct!");
                }
            }
        }
    }
}
