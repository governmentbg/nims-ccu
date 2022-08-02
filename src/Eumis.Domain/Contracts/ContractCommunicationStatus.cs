using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractCommunicationStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ContractCommunicationStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractCommunicationStatus_Sent), ResourceType = typeof(DomainEnumTexts))]
        Sent = 2,
    }
}
