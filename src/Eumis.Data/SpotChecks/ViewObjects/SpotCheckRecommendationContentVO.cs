using Eumis.Common.Json;
using Eumis.Domain.SpotChecks;
using Newtonsoft.Json;

namespace Eumis.Data.SpotChecks.ViewObjects
{
    public class SpotCheckRecommendationContentVO
    {
        public int Id { get; set; }

        public int OrderNum { get; set; }

        public string RecommendationContent { get; set; }
    }
}
