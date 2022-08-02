using Eumis.Common.Json;

namespace Eumis.Domain.SpotChecks
{
    public enum SpotCheckAscertainmentStatus
    {
        [Description(Description = nameof(DomainEnumTexts.SpotCheckAscertainmentStatus_Open), ResourceType = typeof(DomainEnumTexts))]
        Open = 1,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckAscertainmentStatus_Closed), ResourceType = typeof(DomainEnumTexts))]
        Closed = 2,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckAscertainmentStatus_Info), ResourceType = typeof(DomainEnumTexts))]
        Info = 3,
    }
}
