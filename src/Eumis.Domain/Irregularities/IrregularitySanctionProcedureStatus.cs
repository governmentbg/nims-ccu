using Eumis.Common.Json;

namespace Eumis.Domain.Irregularities
{
    public enum IrregularitySanctionProcedureStatus
    {
        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureStatus_Initiated), ResourceType = typeof(DomainEnumTexts))]
        Initiated = 1,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureStatus_Completed), ResourceType = typeof(DomainEnumTexts))]
        Completed = 2,

        [Description(Description = nameof(DomainEnumTexts.IrregularitySanctionProcedureStatus_Abandoned), ResourceType = typeof(DomainEnumTexts))]
        Abandoned = 3,
    }
}
