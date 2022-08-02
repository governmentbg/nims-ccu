using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum Quarter
    {
        [Description(Description = nameof(DomainEnumTexts.Quarter_Q1), ResourceType = typeof(DomainEnumTexts))]
        Q1 = 1,

        [Description(Description = nameof(DomainEnumTexts.Quarter_Q2), ResourceType = typeof(DomainEnumTexts))]
        Q2 = 2,

        [Description(Description = nameof(DomainEnumTexts.Quarter_Q3), ResourceType = typeof(DomainEnumTexts))]
        Q3 = 3,

        [Description(Description = nameof(DomainEnumTexts.Quarter_Q4), ResourceType = typeof(DomainEnumTexts))]
        Q4 = 4,
    }
}
