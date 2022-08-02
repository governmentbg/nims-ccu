using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl
{
    public enum PrognosisStatus
    {
        [Description(Description = nameof(DomainEnumTexts.PrognosisStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.PrognosisStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.PrognosisStatus_Deleted), ResourceType = typeof(DomainEnumTexts))]
        Deleted = 3,
    }
}
