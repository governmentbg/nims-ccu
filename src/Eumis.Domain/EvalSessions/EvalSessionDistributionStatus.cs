using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionDistributionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionDistributionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionDistributionStatus_Applied), ResourceType = typeof(DomainEnumTexts))]
        Applied = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionDistributionStatus_Refused), ResourceType = typeof(DomainEnumTexts))]
        Refused = 3,
    }
}