using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectCommunicationAnswerStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationAnswerStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationAnswerStatus_AnswerFinalized), ResourceType = typeof(DomainEnumTexts))]
        AnswerFinalized = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationAnswerStatus_Answer), ResourceType = typeof(DomainEnumTexts))]
        Answer = 3,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationAnswerStatus_PaperAnswer), ResourceType = typeof(DomainEnumTexts))]
        PaperAnswer = 4,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationAnswerStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 5,
    }
}
