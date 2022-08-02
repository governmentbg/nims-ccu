using Eumis.Common.Json;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckTargetVO
    {
        public int TargetId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckTargetType Type { get; set; }

        public string Name { get; set; }
    }
}
