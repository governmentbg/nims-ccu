using Eumis.Common.Json;

namespace Eumis.Domain.Core
{
    public enum YesNoNonApplicable
    {
        [Description(Description = nameof(DomainEnumTexts.YesNoNonApplicable_Yes), ResourceType = typeof(DomainEnumTexts))]
        Yes = 1,

        [Description(Description = nameof(DomainEnumTexts.YesNoNonApplicable_No), ResourceType = typeof(DomainEnumTexts))]
        No = 2,

        [Description(Description = nameof(DomainEnumTexts.YesNoNonApplicable_NotApplicable), ResourceType = typeof(DomainEnumTexts))]
        NotApplicable = 3,
    }
}
