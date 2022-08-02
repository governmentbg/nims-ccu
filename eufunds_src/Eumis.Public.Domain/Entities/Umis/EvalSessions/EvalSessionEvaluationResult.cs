using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.EvalSessions
{
    public enum EvalSessionEvaluationResult
    {
        [LocalizableDescription("EvalSessionEvaluationResult_Passed")]
        Passed = 1,

        [LocalizableDescription("EvalSessionEvaluationResult_NotPassed")]
        NotPassed = 2
    }
}
