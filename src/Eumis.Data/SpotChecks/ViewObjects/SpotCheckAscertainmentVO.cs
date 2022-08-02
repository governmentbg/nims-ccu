using Eumis.Common.Json;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckAscertainmentVO
    {
        public int AscertainmentId { get; set; }

        public int SpotCheckId { get; set; }

        public int OrderNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckAscertainmentStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckAscertainmentType? Type { get; set; }

        public string Ascertainment { get; set; }
    }
}
