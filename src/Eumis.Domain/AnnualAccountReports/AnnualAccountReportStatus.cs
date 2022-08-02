using Eumis.Common.Json;

namespace Eumis.Domain.AnnualAccountReports
{
    public enum AnnualAccountReportStatus
    {
        [Description(Description = nameof(DomainEnumTexts.AnnualAccountReportStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.AnnualAccountReportStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,

        [Description(Description = nameof(DomainEnumTexts.AnnualAccountReportStatus_Opened), ResourceType = typeof(DomainEnumTexts))]
        Opened = 3,
    }
}
