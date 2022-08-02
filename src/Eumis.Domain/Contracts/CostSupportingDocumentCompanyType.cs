using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum CostSupportingDocumentCompanyType
    {
        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentCompanyType_Beneficiary), ResourceType = typeof(DomainEnumTexts))]
        Beneficiary = 1,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentCompanyType_Partner), ResourceType = typeof(DomainEnumTexts))]
        Partner = 2,

        [Description(Description = nameof(DomainEnumTexts.CostSupportingDocumentCompanyType_Contractor), ResourceType = typeof(DomainEnumTexts))]
        Contractor = 3,
    }
}