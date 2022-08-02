using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportRevalidationCertAuthorityFinancialCorrectionCSD
    {
        public void UpdateAttributes(
            Sign? sign,
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

            this.Sign = sign;
            this.Notes = notes;
            this.RevalidatedEuAmount = revalidatedEuAmount;
            this.RevalidatedBgAmount = revalidatedBgAmount;
            this.RevalidatedBfpTotalAmount = revalidatedBfpTotalAmount;
            this.RevalidatedSelfAmount = revalidatedSelfAmount;
            this.RevalidatedTotalAmount = revalidatedTotalAmount;

            this.ModifyDate = DateTime.Now;
        }

        private void ValidateTotalAmount(decimal? euAmount, decimal? bgAmount, decimal? bfpTotalAmount, decimal? selfAmount, decimal? totalAmount, string type)
        {
            if (euAmount.HasValue && bgAmount.HasValue)
            {
                if ((euAmount + bgAmount) != bfpTotalAmount)
                {
                    throw new DomainException("ContractReportRevalidationCertAuthorityFinancialCorrectionCSD total " + type + " amount is not correct!");
                }

                if (selfAmount.HasValue && ((euAmount + bgAmount + selfAmount) != totalAmount))
                {
                    throw new DomainException("ContractReportRevalidationCertAuthorityFinancialCorrectionCSD total " + type + " amount is not correct!");
                }
            }
        }
    }
}
