using Eumis.Common.Json;

namespace Eumis.Domain.Guidances
{
    public enum GuidanceModule
    {
        [Description(Description = nameof(DomainEnumTexts.GuidanceModule_InternalSystem), ResourceType = typeof(DomainEnumTexts))]
        InternalSystem = 1,

        [Description(Description = nameof(DomainEnumTexts.GuidanceModule_ApplicationsPortal), ResourceType = typeof(DomainEnumTexts))]
        ApplicationsPortal = 2,

        [Description(Description = nameof(DomainEnumTexts.GuidanceModule_ReportsPortal), ResourceType = typeof(DomainEnumTexts))]
        ReportsPortal = 3,
    }
}
