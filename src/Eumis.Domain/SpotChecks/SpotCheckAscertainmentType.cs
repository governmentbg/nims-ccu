using Eumis.Common.Json;

namespace Eumis.Domain.SpotChecks
{
    public enum SpotCheckAscertainmentType
    {
        [Description(Description = nameof(DomainEnumTexts.SpotCheckAscertainmentType_Primary), ResourceType = typeof(DomainEnumTexts))]
        Primary = 1,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckAscertainmentType_Secondary), ResourceType = typeof(DomainEnumTexts))]
        Secondary = 2,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckAscertainmentType_Minor), ResourceType = typeof(DomainEnumTexts))]
        Minor = 3,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckAscertainmentType_Info), ResourceType = typeof(DomainEnumTexts))]
        Info = 4,
    }
}
