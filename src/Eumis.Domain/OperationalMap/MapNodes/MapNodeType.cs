using Eumis.Common.Json;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public enum MapNodeType
    {
        [Description(Description = nameof(DomainEnumTexts.MapNodeType_Programme), ResourceType = typeof(DomainEnumTexts))]
        Programme = 1,

        [Description(Description = nameof(DomainEnumTexts.MapNodeType_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 2,

        [Description(Description = nameof(DomainEnumTexts.MapNodeType_InvestmentPriority), ResourceType = typeof(DomainEnumTexts))]
        InvestmentPriority = 3,

        [Description(Description = nameof(DomainEnumTexts.MapNodeType_SpecificTarget), ResourceType = typeof(DomainEnumTexts))]
        SpecificTarget = 4,
    }
}
