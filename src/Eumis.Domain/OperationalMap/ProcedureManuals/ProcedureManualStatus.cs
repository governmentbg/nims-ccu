using Eumis.Common.Json;

namespace Eumis.Domain.OperationalMap.ProcedureManuals
{
    public enum ProgrammeProcedureManualStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProgrammeProcedureManualStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProgrammeProcedureManualStatus_Actual), ResourceType = typeof(DomainEnumTexts))]
        Actual = 2,

        [Description(Description = nameof(DomainEnumTexts.ProgrammeProcedureManualStatus_Archived), ResourceType = typeof(DomainEnumTexts))]
        Archived = 3,
    }
}
