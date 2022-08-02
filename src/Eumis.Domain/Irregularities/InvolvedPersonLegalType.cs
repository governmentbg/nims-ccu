using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum InvolvedPersonLegalType
    {
        [Description(Description = nameof(DomainEnumTexts.InvolvedPersonLegalType_Person), ResourceType = typeof(DomainEnumTexts))]
        Person = 1,

        [Description(Description = nameof(DomainEnumTexts.InvolvedPersonLegalType_LegalPerson), ResourceType = typeof(DomainEnumTexts))]
        LegalPerson = 2,
    }
}
