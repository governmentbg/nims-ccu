using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectCommunicationStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_DraftQuestion), ResourceType = typeof(DomainEnumTexts))]
        DraftQuestion = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_Question), ResourceType = typeof(DomainEnumTexts))]
        Question = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_DraftAnswer), ResourceType = typeof(DomainEnumTexts))]
        DraftAnswer = 3,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_AnswerFinalized), ResourceType = typeof(DomainEnumTexts))]
        AnswerFinalized = 4,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_Answer), ResourceType = typeof(DomainEnumTexts))]
        Answer = 5,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_PaperAnswer), ResourceType = typeof(DomainEnumTexts))]
        PaperAnswer = 6,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_Applied), ResourceType = typeof(DomainEnumTexts))]
        Applied = 7,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_Rejected), ResourceType = typeof(DomainEnumTexts))]
        Rejected = 8,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 9,

        [Description(Description = nameof(DomainEnumTexts.ProjectCommunicationStatus_Expired), ResourceType = typeof(DomainEnumTexts))]
        Expired = 10,
    }
}
