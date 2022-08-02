using Eumis.Common.Json;

namespace Eumis.Domain.SpotChecks
{
    public enum SpotCheckStatus
    {
        [Description(Description = nameof(DomainEnumTexts.SpotCheckStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.SpotCheckStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,
    }
}
