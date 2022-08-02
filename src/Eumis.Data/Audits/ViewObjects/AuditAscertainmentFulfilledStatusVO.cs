using Eumis.Common.Json;
using Eumis.Domain.CertAuthorityChecks;
using Newtonsoft.Json;

namespace Eumis.Data.Audits.ViewObjects
{
    public class AuditAscertainmentFulfilledStatusVO
    {
        public int Id { get; set; }

        public string RecommendationsFulfilledStatus { get; set; }

        public string IsFinancial { get; set; }
    }
}
