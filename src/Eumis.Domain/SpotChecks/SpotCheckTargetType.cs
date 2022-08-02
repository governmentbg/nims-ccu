using Eumis.Common.Json;

namespace Eumis.Domain.SpotChecks
{
    public enum SpotCheckTargetType
    {
        [Description(Description = nameof(DomainEnumTexts.SpotCheckTargetType_Beneficiary), ResourceType = typeof(DomainEnumTexts))]
        Beneficiary = 1,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckTargetType_Executor), ResourceType = typeof(DomainEnumTexts))]
        Executor = 2,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckTargetType_EndUser), ResourceType = typeof(DomainEnumTexts))]
        EndUser = 3,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckTargetType_Partner), ResourceType = typeof(DomainEnumTexts))]
        Partner = 4,
    }
}
