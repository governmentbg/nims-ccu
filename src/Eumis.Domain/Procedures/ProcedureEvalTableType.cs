using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureEvalTableType
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalTableType_AdminAdmiss), ResourceType = typeof(DomainEnumTexts))]
        AdminAdmiss = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalTableType_TechFinance), ResourceType = typeof(DomainEnumTexts))]
        TechFinance = 2,

        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalTableType_Complex), ResourceType = typeof(DomainEnumTexts))]
        Complex = 3,
    }
}
