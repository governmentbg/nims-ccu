using Eumis.Common.Json;

namespace Eumis.Domain.CertReports
{
    public enum CertReportType
    {
        [Description(Description = nameof(DomainEnumTexts.CertReportType_Intermediate), ResourceType = typeof(DomainEnumTexts))]
        Intermediate = 1,

        [Description(Description = nameof(DomainEnumTexts.CertReportType_Final), ResourceType = typeof(DomainEnumTexts))]
        Final = 2,

        [Description(Description = nameof(DomainEnumTexts.CertReportType_Yearly), ResourceType = typeof(DomainEnumTexts))]
        Yearly = 3,
    }
}