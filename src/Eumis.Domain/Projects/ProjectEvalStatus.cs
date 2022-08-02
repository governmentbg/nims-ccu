using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectEvalStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectEvalStatus_Evaluation), ResourceType = typeof(DomainEnumTexts))]
        Evaluation = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectEvalStatus_Evaluated), ResourceType = typeof(DomainEnumTexts))]
        Evaluated = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectEvalStatus_Contracted), ResourceType = typeof(DomainEnumTexts))]
        Contracted = 3,

        [Description(Description = nameof(DomainEnumTexts.ProjectEvalStatus_PendingApproval), ResourceType = typeof(DomainEnumTexts))]
        PendingApproval = 4,
    }
}
