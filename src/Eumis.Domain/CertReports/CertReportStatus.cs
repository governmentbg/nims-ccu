using Eumis.Common.Json;

namespace Eumis.Domain.CertReports
{
    public enum CertReportStatus
    {
        [Description(Description = nameof(DomainEnumTexts.CertReportStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.CertReportStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,

        [Description(Description = nameof(DomainEnumTexts.CertReportStatus_Unchecked), ResourceType = typeof(DomainEnumTexts))]
        Unchecked = 3,

        [Description(Description = nameof(DomainEnumTexts.CertReportStatus_Approved), ResourceType = typeof(DomainEnumTexts))]
        Approved = 4,

        [Description(Description = nameof(DomainEnumTexts.CertReportStatus_PartialyApproved), ResourceType = typeof(DomainEnumTexts))]
        PartialyApproved = 5,

        [Description(Description = nameof(DomainEnumTexts.CertReportStatus_Unapproved), ResourceType = typeof(DomainEnumTexts))]
        Unapproved = 6,

        [Description(Description = nameof(DomainEnumTexts.CertReportStatus_Returned), ResourceType = typeof(DomainEnumTexts))]
        Returned = 7,
    }
}