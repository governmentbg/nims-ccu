using Eumis.Common.Json;

namespace Eumis.Domain.HistoricContracts
{
    public enum HistoricContractPartnerType
    {
        [Description(Description = nameof(DomainEnumTexts.HistoricContractPartnerType_Partner), ResourceType = typeof(DomainEnumTexts))]
        Partner = 1,

        [Description(Description = nameof(DomainEnumTexts.HistoricContractPartnerType_Contractor), ResourceType = typeof(DomainEnumTexts))]
        Contractor = 2,

        [Description(Description = nameof(DomainEnumTexts.HistoricContractPartnerType_Subcontractor), ResourceType = typeof(DomainEnumTexts))]
        Subcontractor = 3,

        [Description(Description = nameof(DomainEnumTexts.HistoricContractPartnerType_MembersOfUnification), ResourceType = typeof(DomainEnumTexts))]
        Member = 4,
    }
}
