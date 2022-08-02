using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ActiveStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ActiveStatus_NotActivated), ResourceType = typeof(DomainEnumTexts))]
        NotActivated = 1,

        [Description(Description = nameof(DomainEnumTexts.ActiveStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,

        [Description(Description = nameof(DomainEnumTexts.ActiveStatus_Inactive), ResourceType = typeof(DomainEnumTexts))]
        Inactive = 3,
    }
}
