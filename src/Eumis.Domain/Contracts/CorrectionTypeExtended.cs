using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum CorrectionTypeExtended
    {
        [Description(Description = nameof(DomainEnumTexts.CorrectionTypeExtended_FinancialCorrection), ResourceType = typeof(DomainEnumTexts))]
        FinancialCorrection = 1,

        [Description(Description = nameof(DomainEnumTexts.CorrectionTypeExtended_Irregularity), ResourceType = typeof(DomainEnumTexts))]
        Irregularity = 2,

        [Description(Description = nameof(DomainEnumTexts.CorrectionTypeExtended_FinancialCorrectionIrregularity), ResourceType = typeof(DomainEnumTexts))]
        FinancialCorrectionIrregularity = 3,

        [Description(Description = nameof(DomainEnumTexts.CorrectionTypeExtended_FlatFinancialCorrection), ResourceType = typeof(DomainEnumTexts))]
        FlatFinancialCorrection = 4,
    }
}
