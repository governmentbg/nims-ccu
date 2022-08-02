using Eumis.Common.Json;

namespace Eumis.Domain.Audits
{
    public enum AuditLevel
    {
        [Description(Description = nameof(DomainEnumTexts.AuditLevel_Programme), ResourceType = typeof(DomainEnumTexts))]
        Programme = 1,

        [Description(Description = nameof(DomainEnumTexts.AuditLevel_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 2,

        [Description(Description = nameof(DomainEnumTexts.AuditLevel_Procedure), ResourceType = typeof(DomainEnumTexts))]
        Procedure = 3,

        [Description(Description = nameof(DomainEnumTexts.AuditLevel_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 4,

        [Description(Description = nameof(DomainEnumTexts.AuditLevel_ContractContract), ResourceType = typeof(DomainEnumTexts))]
        ContractContract = 5,
    }
}
