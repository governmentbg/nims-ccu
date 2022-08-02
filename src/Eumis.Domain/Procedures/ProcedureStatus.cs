using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureStatus_Entered), ResourceType = typeof(DomainEnumTexts))]
        Entered = 2,

        [Description(Description = nameof(DomainEnumTexts.ProcedureStatus_Checked), ResourceType = typeof(DomainEnumTexts))]
        Checked = 3,

        [Description(Description = nameof(DomainEnumTexts.ProcedureStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 4,

        [Description(Description = nameof(DomainEnumTexts.ProcedureStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 5,

        [Description(Description = nameof(DomainEnumTexts.ProcedureStatus_Terminated), ResourceType = typeof(DomainEnumTexts))]
        Terminated = 6,

        [Description(Description = nameof(DomainEnumTexts.ProcedureStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 7,
    }
}
