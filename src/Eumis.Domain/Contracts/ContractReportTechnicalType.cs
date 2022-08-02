using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum ContractReportTechnicalType
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalType_Intermediate), ResourceType = typeof(DomainEnumTexts))]
        Intermediate = 1,

        [Description(Description = nameof(DomainEnumTexts.ContractReportTechnicalType_Final), ResourceType = typeof(DomainEnumTexts))]
        Final = 2,
    }
}