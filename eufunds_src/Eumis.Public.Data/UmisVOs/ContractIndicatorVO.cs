using Eumis.Public.Common.Localization;

namespace Eumis.Public.Data.UmisVOs
{
    public class ContractIndicatorVO
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

        public string MeasureShortName { get; set; }

        public string MeasureNameAlt { get; set; }

        public string TransMeasureName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English && !string.IsNullOrEmpty(this.MeasureNameAlt))
                {
                    return this.MeasureNameAlt;
                }
                else
                {
                    return this.MeasureShortName;
                }
            }
        }

        public decimal BaseTotalValue { get; set; }

        public decimal TargetAmount { get; set; }

        public decimal CumulativeAmountTotal { get; set; }
    }
}
