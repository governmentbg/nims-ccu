using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureEvalType
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalType_Rejection), ResourceType = typeof(DomainEnumTexts))]
        Rejection = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalType_Weight), ResourceType = typeof(DomainEnumTexts))]
        Weight = 2,
    }
}
