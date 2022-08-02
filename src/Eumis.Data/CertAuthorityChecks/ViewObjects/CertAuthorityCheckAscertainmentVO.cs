using Eumis.Common.Json;
using Eumis.Domain.CertAuthorityChecks;
using Newtonsoft.Json;

namespace Eumis.Data.CertAuthorityChecks.ViewObjects
{
    public class CertAuthorityCheckAscertainmentVO
    {
        public int OrderNum { get; set; }

        public int AscertainmentId { get; set; }

        public int CertAuthorityCheckId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertAuthorityCheckAscertainmentStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertAuthorityCheckAscertainmentType? Type { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertAuthorityCheckAscertainmentExecutionStatus? RecommendationExecutionStatus { get; set; }
    }
}
