using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Domain.Indicators;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class PhysicalExecutionReportItem
    {
        public PhysicalExecutionReportItem()
        {
            this.PeriodAmounts = new Dictionary<int, MonitoringIndicatorPeriodAmountVO>();
        }

        public int IndicatorId { get; set; }

        public string Programme { get; set; }

        public int? ProgrammePriorityId { get; set; }

        public string ProgrammePriority { get; set; }

        public int? InvestmentPriorityId { get; set; }

        public string InvestmentPriority { get; set; }

        public int? SpecificTargetId { get; set; }

        public string SpecificTarget { get; set; }

        public string IndicatorCode { get; set; }

        public string IndicatorName { get; set; }

        public string CommonBaseIndicator { get; set; }

        public string IndicatorMeasure { get; set; }

        public string Measure { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public RegionCategory? RegionCategory { get; set; }

        public decimal? BaseTotalValue { get; set; }

        public int? BaseYear { get; set; }

        public decimal? MilestoneTargetMenValue { get; set; }

        public decimal? MilestoneTargetWomenValue { get; set; }

        public decimal? MilestoneTargetTotalValue { get; set; }

        public decimal? FinalTargetMenValue { get; set; }

        public decimal? FinalTargetWomenValue { get; set; }

        public decimal? FinalTargetTotalValue { get; set; }

        public decimal? TargetMenValue { get; set; }

        public decimal? TargetWomenValue { get; set; }

        public decimal? TargetTotalValue { get; set; }

        public decimal? CumulativeMenValue { get; set; }

        public decimal? CumulativeWomenValue { get; set; }

        public decimal? CumulativeTotalValue { get; set; }

        public int? AchievementsMenProportion { get; set; }

        public int? AchievementsWomenProportion { get; set; }

        public int? AchievementsTotalProportion { get; set; }

        public Dictionary<int, MonitoringIndicatorPeriodAmountVO> PeriodAmounts { get; set; }
    }
}
