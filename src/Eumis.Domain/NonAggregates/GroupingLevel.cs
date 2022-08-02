using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum GroupingLevel
    {
        [Description(Description = nameof(DomainEnumTexts.GroupingLevel_Programme), ResourceType = typeof(DomainEnumTexts))]
        Programme = 1,

        [Description(Description = nameof(DomainEnumTexts.GroupingLevel_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 2,

        [Description(Description = nameof(DomainEnumTexts.GroupingLevel_Procedure), ResourceType = typeof(DomainEnumTexts))]
        Procedure = 3,

        [Description(Description = nameof(DomainEnumTexts.GroupingLevel_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 4,
    }
}