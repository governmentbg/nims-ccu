using System;
using Eumis.Common.Json;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckRecommendationItemVO
    {
        public int? ItemId { get; set; }

        public int RecommendationId { get; set; }

        public int OrderNumber { get; set; }

        public string Recommendation { get; set; }

        public DateTime? Deadline { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public SpotCheckRecommendationStatus? ExecutionStatus { get; set; }

        public DateTime? StatusDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public DateTime? ExecutionProofDate { get; set; }
    }
}
