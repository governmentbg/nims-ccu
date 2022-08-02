using Eumis.Common.Json;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections
{
    public enum FlatFinancialCorrectionLevel
    {
        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionLevel_Programme), ResourceType = typeof(DomainEnumTexts))]
        Programme = 1,

        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionLevel_ProgrammePriority), ResourceType = typeof(DomainEnumTexts))]
        ProgrammePriority = 2,

        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionLevel_Procedure), ResourceType = typeof(DomainEnumTexts))]
        Procedure = 3,

        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionLevel_Contract), ResourceType = typeof(DomainEnumTexts))]
        Contract = 4,

        [Description(Description = nameof(DomainEnumTexts.FlatFinancialCorrectionLevel_ContractContract), ResourceType = typeof(DomainEnumTexts))]
        ContractContract = 5,
    }
}