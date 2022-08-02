using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum AllowedRegistrationType
    {
        [Description(Description = nameof(DomainEnumTexts.AllowedRegistrationType_Digital), ResourceType = typeof(DomainEnumTexts))]
        Digital = 1,

        [Description(Description = nameof(DomainEnumTexts.AllowedRegistrationType_Paper), ResourceType = typeof(DomainEnumTexts))]
        Paper = 2,

        [Description(Description = nameof(DomainEnumTexts.AllowedRegistrationType_DigitalOrPaper), ResourceType = typeof(DomainEnumTexts))]
        DigitalOrPaper = 3,
    }
}
