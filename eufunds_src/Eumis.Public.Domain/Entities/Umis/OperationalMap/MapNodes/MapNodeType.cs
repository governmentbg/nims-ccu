using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.OperationalMap.MapNodes
{
    public enum MapNodeType
    {
        [Description("Програма")]
        Programme = 1,

        [Description("Приоритетна ос")]
        ProgrammePriority = 2,

        [Description("Инвестиционен приоритет")]
        InvestmentPriority = 3,

        [Description("Специфична цел")]
        SpecificTarget = 4,
    }
}
