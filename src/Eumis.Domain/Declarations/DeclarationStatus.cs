using Eumis.Common.Json;

namespace Eumis.Domain
{
    public enum DeclarationStatus
    {
        [Description(Description = nameof(DomainEnumTexts.DeclarationStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.DeclarationStatus_Published), ResourceType = typeof(DomainEnumTexts))]
        Published = 2,

        [Description(Description = nameof(DomainEnumTexts.DeclarationStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}
