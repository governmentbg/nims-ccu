using Eumis.Common.Json;

namespace Eumis.Domain.Core
{
    public enum Sign
    {
        [Description(Description = nameof(DomainEnumTexts.Sign_Negative), ResourceType = typeof(DomainEnumTexts))]
        Negative = -1,

        [Description(Description = nameof(DomainEnumTexts.Sign_Positive), ResourceType = typeof(DomainEnumTexts))]
        Positive = 1,
    }
}
