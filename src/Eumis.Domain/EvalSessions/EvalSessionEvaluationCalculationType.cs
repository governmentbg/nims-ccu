using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionEvaluationCalculationType
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionEvaluationCalculationType_Automatic), ResourceType = typeof(DomainEnumTexts))]
        Automatic = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionEvaluationCalculationType_Manual), ResourceType = typeof(DomainEnumTexts))]
        Manual = 2,
    }
}