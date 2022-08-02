using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularityRapporteur
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularityRapporteur_MTITC), ResourceType = typeof(DomainEnumTexts))]
        MTITC = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularityRapporteur_MRDPW), ResourceType = typeof(DomainEnumTexts))]
        MRDPW = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularityRapporteur_ME), ResourceType = typeof(DomainEnumTexts))]
        ME = 3,

        [Description(Description = nameof(DomainEnumTexts.IrregularityRapporteur_MEW), ResourceType = typeof(DomainEnumTexts))]
        MEW = 4,

        [Description(Description = nameof(DomainEnumTexts.IrregularityRapporteur_MLSP), ResourceType = typeof(DomainEnumTexts))]
        MLSP = 5,

        [Description(Description = nameof(DomainEnumTexts.IrregularityRapporteur_MES), ResourceType = typeof(DomainEnumTexts))]
        MES = 6,

        [Description(Description = nameof(DomainEnumTexts.IrregularityRapporteur_CM), ResourceType = typeof(DomainEnumTexts))]
        CM = 7,

        [Description(Description = nameof(DomainEnumTexts.IrregularityRapporteur_MAFF), ResourceType = typeof(DomainEnumTexts))]
        MAFF = 8,

        [Description(Description = nameof(DomainEnumTexts.IrregularityRapporteur_FEADOP), ResourceType = typeof(DomainEnumTexts))]
        FEADOP = 9,
    }
}
