using Eumis.Common.Json;

namespace Eumis.Domain
{
    public enum NewsStatus
    {
        [Description(Description = nameof(DomainEnumTexts.NewsStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.NewsStatus_Published), ResourceType = typeof(DomainEnumTexts))]
        Published = 2,

        [Description(Description = nameof(DomainEnumTexts.NewsStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}
