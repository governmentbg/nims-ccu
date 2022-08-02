using Eumis.Common.Json;

namespace Eumis.Domain.Registrations
{
    public enum RegProjectXmlRegType
    {
        [Description(Description = nameof(DomainEnumTexts.RegProjectXmlRegType_Digital), ResourceType = typeof(DomainEnumTexts))]
        Digital = 1,

        [Description(Description = nameof(DomainEnumTexts.RegProjectXmlRegType_Paper), ResourceType = typeof(DomainEnumTexts))]
        Paper = 2,
    }
}
