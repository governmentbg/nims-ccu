using Eumis.Common.Json;

namespace Eumis.Domain.Audits
{
    public enum AuditItemLevel
    {
        [Description(Description = nameof(DomainEnumTexts.AuditItemLevel_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 1,

        [Description(Description = nameof(DomainEnumTexts.AuditItemLevel_Procedure), ResourceType = typeof(DomainEnumTexts))]
        Procedure = 2,

        [Description(Description = nameof(DomainEnumTexts.AuditItemLevel_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 3,

        [Description(Description = nameof(DomainEnumTexts.AuditItemLevel_ContractContract), ResourceType = typeof(DomainEnumTexts))]
        ContractContract = 4,
    }
}
