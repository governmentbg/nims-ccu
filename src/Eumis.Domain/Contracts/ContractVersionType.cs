using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractVersionType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractVersionType_NewContract), ResourceType = typeof(DomainEnumTexts))]
        NewContract = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractVersionType_Annex), ResourceType = typeof(DomainEnumTexts))]
        Annex = 2,

        [Description(Description = nameof(DomainEnumTexts.ContractVersionType_Change), ResourceType = typeof(DomainEnumTexts))]
        Change = 3,

        [Description(Description = nameof(DomainEnumTexts.ContractVersionType_PartialAnnex), ResourceType = typeof(DomainEnumTexts))]
        PartialAnnex = 4,

        [Description(Description = nameof(DomainEnumTexts.ContractVersionType_PartialChange), ResourceType = typeof(DomainEnumTexts))]
        PartialChange = 5,
    }
}
