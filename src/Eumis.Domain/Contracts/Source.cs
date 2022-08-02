using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum Source
    {
        [Description(Description = nameof(DomainEnumTexts.Source_Beneficiary), ResourceType = typeof(DomainEnumTexts))]
        Beneficiary = 1,

        [Description(Description = nameof(DomainEnumTexts.Source_AdministrativeAuthority), ResourceType = typeof(DomainEnumTexts))]
        AdministrativeAuthority = 2,
    }
}
