using Eumis.Common.Json;
using Eumis.Domain.CertAuthorityChecks;
using Newtonsoft.Json;

namespace Eumis.Data.CertAuthorityChecks.ViewObjects
{
    public class CertAuthorityCheckAscertainmentExecutionStatusVO
    {
        public int AscertainmentId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CertAuthorityCheckAscertainmentExecutionStatus? RecommendationExecutionStatus { get; set; }
    }
}
