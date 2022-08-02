using Eumis.Common.Json;
using Eumis.Domain.Indicators;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.OperationalMap.MapNodes.ViewObjects
{
    public class MapNodeIndicatorVO
    {
        public int MapNodeId { get; set; }

        public int IndicatorId { get; set; }

        public string Name { get; set; }

        public bool HasGenderDivision { get; set; }

        public string MeasureName { get; set; }

        public decimal? BaseTotalValue { get; set; }

        public decimal? BaseMenValue { get; set; }

        public decimal? BaseWomenValue { get; set; }

        public string BaseQualitativeValue { get; set; }

        public int? BaseYear { get; set; }

        public decimal? TargetTotalValue { get; set; }

        public decimal? TargetMenValue { get; set; }

        public decimal? TargetWomenValue { get; set; }

        public string TargetQualitativeValue { get; set; }

        public decimal? MilestoneTargetTotalValue { get; set; }

        public decimal? MilestoneTargetMenValue { get; set; }

        public decimal? MilestoneTargetWomenValue { get; set; }

        public decimal? FinalTargetTotalValue { get; set; }

        public decimal? FinalTargetMenValue { get; set; }

        public decimal? FinalTargetWomenValue { get; set; }

        public string DataSource { get; set; }

        public string ReportingFrequancy { get; set; }
    }
}
