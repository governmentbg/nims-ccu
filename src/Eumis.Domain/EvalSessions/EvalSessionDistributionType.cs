using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionDistributionType
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionDistributionType_Automatic), ResourceType = typeof(DomainEnumTexts))]
        Automatic = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionDistributionType_Manual), ResourceType = typeof(DomainEnumTexts))]
        Manual = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionDistributionType_Continued), ResourceType = typeof(DomainEnumTexts))]
        Continued = 3,
    }
}