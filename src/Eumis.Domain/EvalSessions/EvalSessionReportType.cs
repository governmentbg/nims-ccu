using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionReportType
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportType_Protocol), ResourceType = typeof(DomainEnumTexts))]
        Protocol = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportType_Report), ResourceType = typeof(DomainEnumTexts))]
        Report = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionReportType_Decision), ResourceType = typeof(DomainEnumTexts))]
        Decision = 3,
    }
}
