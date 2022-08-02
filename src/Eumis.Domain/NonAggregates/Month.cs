using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum Month
    {
        [Description(Description = nameof(DomainEnumTexts.Month_January), ResourceType = typeof(DomainEnumTexts))]
        January = 1,

        [Description(Description = nameof(DomainEnumTexts.Month_February), ResourceType = typeof(DomainEnumTexts))]
        February = 2,

        [Description(Description = nameof(DomainEnumTexts.Month_March), ResourceType = typeof(DomainEnumTexts))]
        March = 3,

        [Description(Description = nameof(DomainEnumTexts.Month_April), ResourceType = typeof(DomainEnumTexts))]
        April = 4,

        [Description(Description = nameof(DomainEnumTexts.Month_May), ResourceType = typeof(DomainEnumTexts))]
        May = 5,

        [Description(Description = nameof(DomainEnumTexts.Month_June), ResourceType = typeof(DomainEnumTexts))]
        June = 6,

        [Description(Description = nameof(DomainEnumTexts.Month_July), ResourceType = typeof(DomainEnumTexts))]
        July = 7,

        [Description(Description = nameof(DomainEnumTexts.Month_August), ResourceType = typeof(DomainEnumTexts))]
        August = 8,

        [Description(Description = nameof(DomainEnumTexts.Month_September), ResourceType = typeof(DomainEnumTexts))]
        September = 9,

        [Description(Description = nameof(DomainEnumTexts.Month_October), ResourceType = typeof(DomainEnumTexts))]
        October = 10,

        [Description(Description = nameof(DomainEnumTexts.Month_November), ResourceType = typeof(DomainEnumTexts))]
        November = 11,

        [Description(Description = nameof(DomainEnumTexts.Month_December), ResourceType = typeof(DomainEnumTexts))]
        December = 12,
    }
}
