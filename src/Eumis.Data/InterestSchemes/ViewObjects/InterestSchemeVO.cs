namespace Eumis.Data.InterestSchemes.ViewObjects
{
    public class InterestSchemeVO
    {
        public int InterestSchemeId { get; set; }

        public string Name { get; set; }

        public string BasicInterestRateName { get; set; }

        public string AllowanceName { get; set; }

        public int AnnualBasis { get; set; }

        public bool IsActive { get; set; }
    }
}
