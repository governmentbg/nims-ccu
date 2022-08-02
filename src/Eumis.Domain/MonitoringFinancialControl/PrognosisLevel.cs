using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public enum PrognosisLevel
    {
        [Description(Description = nameof(DomainEnumTexts.PrognosisLevel_Programme), ResourceType = typeof(DomainEnumTexts))]
        Programme = 1,

        [Description(Description = nameof(DomainEnumTexts.PrognosisLevel_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 2,

        [Description(Description = nameof(DomainEnumTexts.PrognosisLevel_Procedure), ResourceType = typeof(DomainEnumTexts))]
        Procedure = 3,
    }
}