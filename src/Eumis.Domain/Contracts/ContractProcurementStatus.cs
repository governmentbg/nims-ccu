using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractProcurementStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractProcurementStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractProcurementStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractProcurementStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractProcurementStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 4,
    }
}
