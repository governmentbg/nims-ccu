using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum NutsLevel
    {
        [Description(Description = nameof(DomainEnumTexts.NutsLevel_Country), ResourceType = typeof(DomainEnumTexts))]
        Country = 1,

        [Description(Description = nameof(DomainEnumTexts.NutsLevel_RegionNUTS1), ResourceType = typeof(DomainEnumTexts))]
        RegionNUTS1 = 2,

        [Description(Description = nameof(DomainEnumTexts.NutsLevel_RegionNUTS2), ResourceType = typeof(DomainEnumTexts))]
        RegionNUTS2 = 3,

        [Description(Description = nameof(DomainEnumTexts.NutsLevel_District), ResourceType = typeof(DomainEnumTexts))]
        District = 4,

        [Description(Description = nameof(DomainEnumTexts.NutsLevel_Municipality), ResourceType = typeof(DomainEnumTexts))]
        Municipality = 5,

        [Description(Description = nameof(DomainEnumTexts.NutsLevel_Settlement), ResourceType = typeof(DomainEnumTexts))]
        Settlement = 6,

        [Description(Description = nameof(DomainEnumTexts.NutsLevel_ProtectedZone), ResourceType = typeof(DomainEnumTexts))]
        ProtectedZone = 7,
    }
}
