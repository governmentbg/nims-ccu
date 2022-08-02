using Eumis.Common.Json;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public enum ProjectsReportItemStandingStatus
    {
        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemStandingStatus_Approved), ResourceType = typeof(DataEnumTexts))]
        Approved = 1,

        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemStandingStatus_Reserve), ResourceType = typeof(DataEnumTexts))]
        Reserve = 2,

        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemStandingStatus_Rejected), ResourceType = typeof(DataEnumTexts))]
        Rejected = 3,

        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemStandingStatus_RejectedAtASD), ResourceType = typeof(DataEnumTexts))]
        RejectedAtASD = 4,

        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemStandingStatus_RejectedAtTFO), ResourceType = typeof(DataEnumTexts))]
        RejectedAtTFO = 5,

        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemStandingStatus_Withdrawed), ResourceType = typeof(DataEnumTexts))]
        Withdrawed = 6,

        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemStandingStatus_Canceled), ResourceType = typeof(DataEnumTexts))]
        Canceled = 7,

        [Description(Description = nameof(DataEnumTexts.ProjectsReportItemStandingStatus_WithoutStanding), ResourceType = typeof(DataEnumTexts))]
        WithoutStanding = 8,
    }
}
