using Eumis.Public.Common.Localization;
using Eumis.Public.Domain.Entities.Umis.Indicators;

namespace Eumis.Public.Data.UmisVOs
{
    public class StatisticIndicatorVO
    {
        public int IndicatorId { get; set; }

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

        public string ProgrammeShortName { get; set; }

        public string ProgrammeShortNameAlt { get; set; }

        public string TransProgrammeShortName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.ProgrammeShortNameAlt) ? this.ProgrammeShortName : this.ProgrammeShortNameAlt;
                }
                else
                {
                    return this.ProgrammeShortName;
                }
            }
        }

        public IndicatorType IndicatorType { get; set; }

        public IndicatorKind IndicatorKind { get; set; }

        public IndicatorTrend IndicatorTrend { get; set; }

        public string MeasuerName { get; set; }

        public string MeasuerNameAlt { get; set; }

        public string TransMeasuerName
        {
            get
            {
                if (SystemLocalization.GetCurrentLanguage() == LocalizationLanguage.English)
                {
                    return string.IsNullOrEmpty(this.MeasuerNameAlt) ? this.MeasuerName : this.MeasuerNameAlt;
                }
                else
                {
                    return this.MeasuerName;
                }
            }
        }

        public IndicatorAggregatedReport AggregatedReport { get; set; }

        public IndicatorAggregatedTarget AggregatedTarget { get; set; }

        public decimal BaseTotalValue { get; set; }

        public decimal TargetTotalValue { get; set; }

        public decimal ApprovedPeriodAmountTotal { get; set; }
    }
}
