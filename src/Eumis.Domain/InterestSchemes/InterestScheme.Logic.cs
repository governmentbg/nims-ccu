using System;

namespace Eumis.Domain.InterestSchemes
{
    public partial class InterestScheme
    {
        public void UpdateInterestScheme(
            string name,
            int basicInterestRateId,
            int allowanceId,
            int annualBasis,
            bool isActive)
        {
            this.Name = name;
            this.BasicInterestRateId = basicInterestRateId;
            this.AllowanceId = allowanceId;
            this.AnnualBasis = annualBasis;
            this.IsActive = isActive;

            this.ModifyDate = DateTime.Now;
        }
    }
}
