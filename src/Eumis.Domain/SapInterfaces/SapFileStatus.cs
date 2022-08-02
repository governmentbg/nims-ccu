using Eumis.Common.Json;

namespace Eumis.Domain.SapInterfaces
{
    public enum SapFileStatus
    {
        [Description(Description = nameof(DomainEnumTexts.SapFileStatus_New), ResourceType = typeof(DomainEnumTexts))]
        New = 1,

        [Description(Description = nameof(DomainEnumTexts.SapFileStatus_Imported), ResourceType = typeof(DomainEnumTexts))]
        Imported = 2,
    }
}
