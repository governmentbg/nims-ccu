using System;
using Eumis.Domain.Indicators;

namespace Eumis.Domain.Procedures.Json
{
    public class IndicatorJson
    {
        public int IndicatorId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string MeasureName { get; set; }

        public string MeasureNameAlt { get; set; }

        public bool HasGenderDivision { get; set; }

        public bool IsActive { get; set; }
    }
}
