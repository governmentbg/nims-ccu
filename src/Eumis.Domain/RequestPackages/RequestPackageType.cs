using Eumis.Common.Json;

namespace Eumis.Domain.RequestPackages
{
    public enum RequestPackageType
    {
        [Description(Description = nameof(DomainEnumTexts.RequestPackageType_Request), ResourceType = typeof(DomainEnumTexts))]
        Request = 1,

        [Description(Description = nameof(DomainEnumTexts.RequestPackageType_DirectChange), ResourceType = typeof(DomainEnumTexts))]
        DirectChange = 2,
    }
}
