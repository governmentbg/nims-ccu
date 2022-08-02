using Eumis.Common.Json;

namespace Eumis.Domain.Contracts
{
    public enum CorrectionType
    {
        [Description(Description = nameof(DomainEnumTexts.CorrectionType_FinancialCorrection), ResourceType = typeof(DomainEnumTexts))]
        FinancialCorrection = 1,

        [Description(Description = nameof(DomainEnumTexts.CorrectionType_Irregularity), ResourceType = typeof(DomainEnumTexts))]
        Irregularity = 2,

        [Description(Description = nameof(DomainEnumTexts.CorrectionType_FinancialCorrectionIrregularity), ResourceType = typeof(DomainEnumTexts))]
        FinancialCorrectionIrregularity = 3,
    }
}
