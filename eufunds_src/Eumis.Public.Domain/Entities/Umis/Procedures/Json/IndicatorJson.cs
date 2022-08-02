using System;
using Eumis.Public.Domain.Entities.Umis.Indicators;

namespace Eumis.Public.Domain.Entities.Umis.Procedures.Json
{
    public class IndicatorJson
    {
        public int IndicatorId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public IndicatorType Type { get; set; }

        public IndicatorKind Kind { get; set; }

        public IndicatorTrend Trend { get; set; }

        public string MeasureName { get; set; }

        public IndicatorAggregatedReport AggregatedReport { get; set; }

        public IndicatorAggregatedTarget AggregatedTarget { get; set; }

        public bool HasGenderDivision { get; set; }

        public bool IsActive { get; set; }
    }
}
