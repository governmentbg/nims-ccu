using Eumis.Common.Json;

namespace Eumis.Domain.RequestPackages
{
    public enum RequestPackageStatus
    {
        [Description(Description = nameof(DomainEnumTexts.RequestPackageStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.RequestPackageStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.RequestPackageStatus_Checked), ResourceType = typeof(DomainEnumTexts))]
        Checked = 3,

        [Description(Description = nameof(DomainEnumTexts.RequestPackageStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 4,

        [Description(Description = nameof(DomainEnumTexts.RequestPackageStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 5,
    }
}
