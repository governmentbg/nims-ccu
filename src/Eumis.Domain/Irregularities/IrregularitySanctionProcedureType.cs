using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularitySanctionProcedureType
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureType_SP1), ResourceType = typeof(DomainEnumTexts))]
        SP1 = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureType_SP2), ResourceType = typeof(DomainEnumTexts))]
        SP2 = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureType_SP3), ResourceType = typeof(DomainEnumTexts))]
        SP3 = 3,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureType_SP4), ResourceType = typeof(DomainEnumTexts))]
        SP4 = 4,
    }
}
