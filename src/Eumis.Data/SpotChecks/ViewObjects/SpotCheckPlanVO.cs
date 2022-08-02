using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckPlanVO
    {
        public int SpotCheckPlanId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Year Year { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Month Month { get; set; }

        public string ProgrammeName { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckLevel Level { get; set; }
    }
}
