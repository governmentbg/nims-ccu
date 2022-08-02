using Eumis.Common.Json;

namespace Eumis.Domain.SpotChecks
{
    public enum SpotCheckLevel
    {
        [Description(Description = nameof(DomainEnumTexts.SpotCheckLevel_Programme), ResourceType = typeof(DomainEnumTexts))]
        Programme = 1,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckLevel_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 2,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckLevel_Procedure), ResourceType = typeof(DomainEnumTexts))]
        Procedure = 3,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckLevel_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 4,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckLevel_ContractContract), ResourceType = typeof(DomainEnumTexts))]
        ContractContract = 5,
    }
}
