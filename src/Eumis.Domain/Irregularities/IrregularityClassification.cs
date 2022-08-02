using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularityClassification
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularityClassification_NoIrregularity), ResourceType = typeof(DomainEnumTexts))]
        NoIrregularity = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularityClassification_Irregularity), ResourceType = typeof(DomainEnumTexts))]
        Irregularity = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularityClassification_FraudSuspicion), ResourceType = typeof(DomainEnumTexts))]
        FraudSuspicion = 3,

        [Description(Description = nameof(DomainEnumTexts.IrregularityClassification_Fraud), ResourceType = typeof(DomainEnumTexts))]
        Fraud = 4,
    }
}
