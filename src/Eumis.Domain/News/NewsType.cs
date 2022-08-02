using Eumis.Common.Json;

namespace Eumis.Domain
{
    public enum NewsType
    {
        [Description(Description = nameof(DomainEnumTexts.NewsType_Internal), ResourceType = typeof(DomainEnumTexts))]
        Internal = 1,

        [Description(Description = nameof(DomainEnumTexts.NewsType_Portal), ResourceType = typeof(DomainEnumTexts))]
        Portal = 2,
    }
}
