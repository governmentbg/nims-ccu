using Eumis.Common.Json;

namespace Eumis.Domain.Procurements
{
    public enum ProcurementStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProcurementStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcurementStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,

        [Description(Description = nameof(DomainEnumTexts.ProcurementStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 3,
    }
}
