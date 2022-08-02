using Eumis.Public.Common.Localization;

namespace Eumis.Public.Data.ProgrammeGroups.ViewObjects
{
    public class ProgrammeFinanceSourceBudgetsVO
    {
        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.NameAlt) ? this.Name : this.NameAlt;
                }
                else
                {
                    return this.Name;
                }
            }
        }

        public decimal? ESFAmount { get; set; }

        public decimal? ERDFAmount { get; set; }

        public decimal? CFAmount { get; set; }

        public decimal? YEIAmount { get; set; }

        public decimal? FEAMDAmount { get; set; }

        public decimal? EFMDRAmount { get; set; }

        public decimal? EZFRSRAmount { get; set; }

        public decimal? FVSAmount { get; set; }

        public decimal? FUMIAmount { get; set; }

        public decimal? OtherAmount { get; set; }

        public decimal? EEAFMAmount { get; set; }

        public decimal? NFMAmount { get; set; }
    }
}
