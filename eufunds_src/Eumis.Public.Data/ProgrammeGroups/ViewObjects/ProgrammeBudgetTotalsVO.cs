using Eumis.Public.Common.Localization;

namespace Eumis.Public.Data.ProgrammeGroups.ViewObjects
{
    public class ProgrammeBudgetTotalsVO
    {
        public string ProgrammeName { get; set; }

        public string ProgrammeNameAlt { get; set; }

        public virtual string TransName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return this.ProgrammeNameAlt;
                }
                else
                {
                    return this.ProgrammeName;
                }
            }
        }

        public decimal TotalAmount { get; set; }
    }
}
