using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum PersonalUinType
    {
        [Description(Description = nameof(DomainEnumTexts.PersonalUinType_PersonalBulstat), ResourceType = typeof(DomainEnumTexts))]
        PersonalBulstat = 1,

        [Description(Description = nameof(DomainEnumTexts.PersonalUinType_ForeignNumber), ResourceType = typeof(DomainEnumTexts))]
        ForeignNumber = 2,
    }
}
