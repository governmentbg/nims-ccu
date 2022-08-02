using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionReportProjectStandingStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportProjectStandingStatus_Approved), ResourceType = typeof(DomainEnumTexts))]
        Approved = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportProjectStandingStatus_Reserve), ResourceType = typeof(DomainEnumTexts))]
        Reserve = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportProjectStandingStatus_Rejected), ResourceType = typeof(DomainEnumTexts))]
        Rejected = 3,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportProjectStandingStatus_RejectedAtASD), ResourceType = typeof(DomainEnumTexts))]
        RejectedAtASD = 4,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportProjectStandingStatus_RejectedAtTFO), ResourceType = typeof(DomainEnumTexts))]
        RejectedAtTFO = 5,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportProjectStandingStatus_Withdrawed), ResourceType = typeof(DomainEnumTexts))]
        Withdrawed = 6,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportProjectStandingStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 7,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportProjectStandingStatus_WithoutStanding), ResourceType = typeof(DomainEnumTexts))]
        WithoutStanding = 8,
    }
}
