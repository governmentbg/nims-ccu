using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum YesNoOther
    {
        [Description(Description = nameof(DomainEnumTexts.YesNoOther_Yes), ResourceType = typeof(DomainEnumTexts))]
        Yes = 1,

        [Description(Description = nameof(DomainEnumTexts.YesNoOther_No), ResourceType = typeof(DomainEnumTexts))]
        No = 2,

        [Description(Description = nameof(DomainEnumTexts.YesNoOther_Other), ResourceType = typeof(DomainEnumTexts))]
        Other = 3,
    }
}
