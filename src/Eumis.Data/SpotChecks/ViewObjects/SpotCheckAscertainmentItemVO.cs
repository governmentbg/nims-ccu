using Eumis.Common.Json;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckAscertainmentItemVO
    {
        public int? ItemId { get; set; }

        public int AscertainmentId { get; set; }

        public int OrderNumber { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckAscertainmentType Type { get; set; }

        public string Ascertainment { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckAscertainmentStatus Status { get; set; }

        public string CheckSubjectComment { get; set; }

        public string ManagingAuthorityComment { get; set; }
    }
}
