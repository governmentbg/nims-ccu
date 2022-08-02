using Eumis.Common.Json;

namespace Eumis.Domain.SpotChecks
{
    public enum SpotCheckRecommendationStatus
    {
        [Description(Description = nameof(DomainEnumTexts.SpotCheckRecommendationStatus_Executed), ResourceType = typeof(DomainEnumTexts))]
        Executed = 1,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckRecommendationStatus_InProcess), ResourceType = typeof(DomainEnumTexts))]
        InProcess = 2,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckRecommendationStatus_Unexecuted), ResourceType = typeof(DomainEnumTexts))]
        Unexecuted = 3,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckRecommendationStatus_Removed), ResourceType = typeof(DomainEnumTexts))]
        Removed = 4,
    }
}
