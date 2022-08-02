using Eumis.Common.Json;

namespace Eumis.Domain.SpotChecks
{
    public enum SpotCheckItemLevel
    {
        [Description(Description = nameof(DomainEnumTexts.SpotCheckItemLevel_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 1,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckItemLevel_Procedure), ResourceType = typeof(DomainEnumTexts))]
        Procedure = 2,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckItemLevel_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 3,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckItemLevel_ContractContract), ResourceType = typeof(DomainEnumTexts))]
        ContractContract = 4,
    }
}
