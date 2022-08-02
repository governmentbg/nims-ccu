using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportCertAuthorityFinancialCorrectionCSD
    {
        public void UpdateAttributes(
            Sign? sign,
            string notes,
            decimal? certifiedEuAmount,
            decimal? certifiedBgAmount,
            decimal? certifiedBfpTotalAmount,
            decimal? certifiedSelfAmount,
            decimal? certifiedTotalAmount)
        {
            this.ValidateTotalAmount(
                certifiedEuAmount,
                certifiedBgAmount,
                certifiedBfpTotalAmount,
                certifiedSelfAmount,
                certifiedTotalAmount,
                "revalidated");

            this.Sign = sign;
            this.Notes = notes;
            this.CertifiedEuAmount = certifiedEuAmount;
            this.CertifiedBgAmount = certifiedBgAmount;
            this.CertifiedBfpTotalAmount = certifiedBfpTotalAmount;
            this.CertifiedSelfAmount = certifiedSelfAmount;
            this.CertifiedTotalAmount = certifiedTotalAmount;

            this.ModifyDate = DateTime.Now;
        }

        private void ValidateTotalAmount(decimal? euAmount, decimal? bgAmount, decimal? bfpTotalAmount, decimal? selfAmount, decimal? totalAmount, string type)
        {
            if (euAmount.HasValue && bgAmount.HasValue)
            {
                if ((euAmount + bgAmount) != bfpTotalAmount)
                {
                    throw new DomainException("ContractReportFinancialRevalidationCSD total " + type + " amount is not correct!");
                }

                if (selfAmount.HasValue && ((euAmount + bgAmount + selfAmount) != totalAmount))
                {
                    throw new DomainException("ContractReportFinancialRevalidationCSD total " + type + " amount is not correct!");
                }
            }
        }
    }
}
