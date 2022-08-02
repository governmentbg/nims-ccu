using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum UinType
    {
        [Description(Description = nameof(DomainEnumTexts.UinType_Eik), ResourceType = typeof(DomainEnumTexts))]
        Eik = 0,

        [Description(Description = nameof(DomainEnumTexts.UinType_Bulstat), ResourceType = typeof(DomainEnumTexts))]
        Bulstat = 1,

        [Description(Description = nameof(DomainEnumTexts.UinType_PersonalBulstat), ResourceType = typeof(DomainEnumTexts))]
        PersonalBulstat = 2,

        [Description(Description = nameof(DomainEnumTexts.UinType_Foreign), ResourceType = typeof(DomainEnumTexts))]
        Foreign = 3,
    }
}
