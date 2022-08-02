using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckPlanInfoVO
    {
        public string ProgrammeCode { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Month Month { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public Year Year { get; set; }

        public SpotCheckLevel Level { get; set; }

        public byte[] Version { get; set; }
    }
}
