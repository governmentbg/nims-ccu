using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionType
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionType_Preselection), ResourceType = typeof(DomainEnumTexts))]
        Preselection = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionType_ProjectEvaluation), ResourceType = typeof(DomainEnumTexts))]
        ProjectEvaluation = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionType_ProjectFicheEvaluation), ResourceType = typeof(DomainEnumTexts))]
        ProjectFicheEvaluation = 3,
    }
}
