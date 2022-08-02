using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionEvaluationResult
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionEvaluationResult_Passed), ResourceType = typeof(DomainEnumTexts))]
        Passed = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionEvaluationResult_NotPassed), ResourceType = typeof(DomainEnumTexts))]
        NotPassed = 2,
    }
}
