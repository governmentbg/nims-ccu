using Eumis.Domain.InterestSchemes;

namespace Eumis.Web.Api.InterestSchemes.DataObjects
{
    public class InterestSchemeDO
    {
        public InterestSchemeDO()
        {
        }

        public InterestSchemeDO(InterestScheme interestScheme)
        {
            this.InterestSchemeId = interestScheme.InterestSchemeId;
            this.Name = interestScheme.Name;
            this.BasicInterestRateId = interestScheme.BasicInterestRateId;
            this.AllowanceId = interestScheme.AllowanceId;
            this.AnnualBasis = interestScheme.AnnualBasis;
            this.IsActive = interestScheme.IsActive;
            this.Version = interestScheme.Version;
        }

        public int? InterestSchemeId { get; set; }

        public string Name { get; set; }

        public int? BasicInterestRateId { get; set; }

        public int? AllowanceId { get; set; }

        public int? AnnualBasis { get; set; }

        public bool? IsActive { get; set; }

        public byte[] Version { get; set; }
    }
}
