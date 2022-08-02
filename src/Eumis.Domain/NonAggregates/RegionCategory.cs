using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum RegionCategory
    {
        [Description(Description = nameof(DomainEnumTexts.RegionCategory_LessDevelopedRegions), ResourceType = typeof(DomainEnumTexts))]
        LessDevelopedRegions = 1,
    }
}