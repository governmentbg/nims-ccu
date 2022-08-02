using Eumis.Common.Json;

namespace Eumis.Data.Registrations.PortalViewObjects
{
    public enum ProjectManagingAuthorityCommunicationPortalStatus
    {
        [Description(Description = nameof(DataEnumTexts.ProjectManagingAuthorityCommunicationPortalStatus_Draft), ResourceType = typeof(DataEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DataEnumTexts.ProjectManagingAuthorityCommunicationPortalStatus_Sent), ResourceType = typeof(DataEnumTexts))]
        Sent = 2,

        [Description(Description = nameof(DataEnumTexts.ProjectManagingAuthorityCommunicationPortalStatus_NotRead), ResourceType = typeof(DataEnumTexts))]
        NotRead = 3,

        [Description(Description = nameof(DataEnumTexts.ProjectManagingAuthorityCommunicationPortalStatus_PendingAnswer), ResourceType = typeof(DataEnumTexts))]
        PendingAnswer = 4,

        [Description(Description = nameof(DataEnumTexts.ProjectManagingAuthorityCommunicationPortalStatus_Answered), ResourceType = typeof(DataEnumTexts))]
        Answered = 5,

        [Description(Description = nameof(DataEnumTexts.ProjectManagingAuthorityCommunicationPortalStatus_Canceled), ResourceType = typeof(DataEnumTexts))]
        Canceled = 6,
    }
}
