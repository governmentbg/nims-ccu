using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionUserType
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionUserType_Administrator), ResourceType = typeof(DomainEnumTexts))]
        Administrator = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionUserType_Assessor), ResourceType = typeof(DomainEnumTexts))]
        Assessor = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionUserType_AssistantAssessor), ResourceType = typeof(DomainEnumTexts))]
        AssistantAssessor = 3,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionUserType_Observer), ResourceType = typeof(DomainEnumTexts))]
        Observer = 4,
    }
}