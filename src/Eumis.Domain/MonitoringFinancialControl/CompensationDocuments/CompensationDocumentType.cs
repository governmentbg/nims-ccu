using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.CompensationDocuments
{
    public enum CompensationDocumentType
    {
        [Description(Description = nameof(DomainEnumTexts.CompensationDocumentType_Contracted), ResourceType = typeof(DomainEnumTexts))]
        Contracted = 1,

        [Description(Description = nameof(DomainEnumTexts.CompensationDocumentType_Requested), ResourceType = typeof(DomainEnumTexts))]
        Requested = 2,

        [Description(Description = nameof(DomainEnumTexts.CompensationDocumentType_ActuallyPaidAmount), ResourceType = typeof(DomainEnumTexts))]
        ActuallyPaidAmount = 3,
    }
}
