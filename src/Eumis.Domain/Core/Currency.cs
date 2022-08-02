using Eumis.Common.Json;

namespace Eumis.Domain.Core
{
    public enum Currency
    {
        [Description(Description = nameof(DomainEnumTexts.Currency_Lev), ResourceType = typeof(DomainEnumTexts))]
        Lev = 1,

        [Description(Description = nameof(DomainEnumTexts.Currency_Euro), ResourceType = typeof(DomainEnumTexts))]
        Euro = 2,
    }
}
