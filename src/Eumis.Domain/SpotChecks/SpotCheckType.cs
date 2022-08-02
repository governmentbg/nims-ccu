using Eumis.Common.Json;

namespace Eumis.Domain.SpotChecks
{
    public enum SpotCheckType
    {
        [Description(Description = nameof(DomainEnumTexts.SpotCheckType_Planned), ResourceType = typeof(DomainEnumTexts))]
        Planned = 1,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckType_NotPlanned), ResourceType = typeof(DomainEnumTexts))]
        NotPlanned = 2,
    }
}
