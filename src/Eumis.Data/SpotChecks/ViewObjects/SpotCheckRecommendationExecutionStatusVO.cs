using Eumis.Common.Json;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckRecommendationExecutionStatusVO
    {
        public int Id { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckRecommendationStatus? ExecutionStatus { get; set; }
    }
}
