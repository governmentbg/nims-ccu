using Eumis.Common.Json;

namespace Eumis.Domain.Projects
{
    public enum ProjectManagingAuthorityCommunicationStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationStatus_DraftQuestion), ResourceType = typeof(DomainEnumTexts))]
        DraftQuestion = 1,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationStatus_Question), ResourceType = typeof(DomainEnumTexts))]
        Question = 2,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 3,

        [Description(Description = nameof(DomainEnumTexts.ProjectManagingAuthorityCommunicationStatus_Expired), ResourceType = typeof(DomainEnumTexts))]
        Expired = 4,
    }
}
