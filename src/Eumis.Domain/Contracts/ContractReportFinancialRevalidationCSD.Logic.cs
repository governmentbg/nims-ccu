using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancialRevalidationCSD
    {
        public void UpdateAttributes(
            string notes,
            decimal? revalidatedEuAmount,
            decimal? revalidatedBgAmount,
            decimal? revalidatedBfpTotalAmount,
            decimal? revalidatedSelfAmount,
            decimal? revalidatedTotalAmount)
        {
            this.ValidateTotalAmount(
                revalidatedEuAmount,
                revalidatedBgAmount,
                revalidatedBfpTotalAmount,
                revalidatedSelfAmount,
                revalidatedTotalAmount,
                "revalidated");

            this.Notes = notes;
            this.RevalidatedEuAmount = revalidatedEuAmount;
            this.RevalidatedBgAmount = revalidatedBgAmount;
            this.RevalidatedBfpTotalAmount = revalidatedBfpTotalAmount;
            this.RevalidatedSelfAmount = revalidatedSelfAmount;
            this.RevalidatedTotalAmount = revalidatedTotalAmount;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateCertAttributes(
            decimal? uncertifiedRevalidatedEuAmount,
            decimal? uncertifiedRevalidatedBgAmount,
            decimal? uncertifiedRevalidatedBfpTotalAmount,
            decimal? uncertifiedRevalidatedSelfAmount,
            decimal? uncertifiedRevalidatedTotalAmount,
            decimal? certifiedRevalidatedEuAmount,
            decimal? certifiedRevalidatedBgAmount,
            decimal? certifiedRevalidatedBfpTotalAmount,
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
            this.UncertifiedRevalidatedSelfAmount = uncertifiedRevalidatedSelfAmount;
            this.UncertifiedRevalidatedTotalAmount = uncertifiedRevalidatedTotalAmount;

            this.CertifiedRevalidatedEuAmount = certifiedRevalidatedEuAmount;
            this.CertifiedRevalidatedBgAmount = certifiedRevalidatedBgAmount;
            this.CertifiedRevalidatedBfpTotalAmount = certifiedRevalidatedBfpTotalAmount;
            this.CertifiedRevalidatedSelfAmount = certifiedRevalidatedSelfAmount;
            this.CertifiedRevalidatedTotalAmount = certifiedRevalidatedTotalAmount;

            this.ModifyDate = DateTime.Now;
        }

        private void ValidateTotalAmount(decimal? euAmount, decimal? bgAmount, decimal? bfpTotalAmount, decimal? selfAmount, decimal? totalAmount, string type)
        {
            if ((euAmount + bgAmount) != bfpTotalAmount)
            {
                throw new DomainException("ContractReportFinancialRevalidationCSD total " + type + " amount is not correct!");
            }

            if (euAmount.HasValue && bgAmount.HasValue && selfAmount.HasValue)
            {
                if ((euAmount + bgAmount + selfAmount) != totalAmount)
                {
                    throw new DomainException("ContractReportFinancialRevalidationCSD total " + type + " amount is not correct!");
                }
            }
        }
    }
}
