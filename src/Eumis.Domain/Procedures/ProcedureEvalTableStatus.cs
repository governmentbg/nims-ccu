using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureEvalTableStatus
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalTableStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalTableStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 2,
    }
}
