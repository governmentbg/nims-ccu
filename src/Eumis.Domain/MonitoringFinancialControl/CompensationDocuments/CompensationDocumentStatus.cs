using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.CompensationDocuments
{
    public enum CompensationDocumentStatus
    {
        [Description(Description = nameof(DomainEnumTexts.CompensationDocumentStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.CompensationDocumentStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.CompensationDocumentStatus_Deleted), ResourceType = typeof(DomainEnumTexts))]
        Deleted = 3,
    }
}
