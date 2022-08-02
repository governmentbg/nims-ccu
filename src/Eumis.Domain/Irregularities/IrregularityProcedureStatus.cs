using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularityProcedureStatus
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularityProcedureStatus_AP), ResourceType = typeof(DomainEnumTexts))]
        AP = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularityProcedureStatus_JP), ResourceType = typeof(DomainEnumTexts))]
        JP = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularityProcedureStatus_PP), ResourceType = typeof(DomainEnumTexts))]
        PP = 3,

        [Description(Description = nameof(DomainEnumTexts.IrregularityProcedureStatus_PX), ResourceType = typeof(DomainEnumTexts))]
        PX = 4,
    }
}
