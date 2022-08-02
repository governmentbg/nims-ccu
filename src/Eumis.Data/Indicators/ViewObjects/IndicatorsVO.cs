using Eumis.Common.Json;
using Eumis.Domain.Indicators;
using Newtonsoft.Json;

namespace Eumis.Data.Indicators.ViewObjects
{
    public class IndicatorsVO
    {
        public int IndicatorId { get; set; }

        public string ProgrammeName { get; set; }

        public string MeasureName { get; set; }

        public string Name { get; set; }

        public bool HasGenderDivision { get; set; }
    }
}
