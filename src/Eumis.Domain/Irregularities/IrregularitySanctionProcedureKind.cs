using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularitySanctionProcedureKind
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureKind_Adm), ResourceType = typeof(DomainEnumTexts))]
        Adm = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureKind_Pen), ResourceType = typeof(DomainEnumTexts))]
        Pen = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureKind_Pxx), ResourceType = typeof(DomainEnumTexts))]
        Pxx = 3,
    }
}
