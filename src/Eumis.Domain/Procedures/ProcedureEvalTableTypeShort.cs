using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureEvalTableTypeShort
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalTableTypeShort_AdminAdmiss), ResourceType = typeof(DomainEnumTexts))]
        AdminAdmiss = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalTableTypeShort_TechFinance), ResourceType = typeof(DomainEnumTexts))]
        TechFinance = 2,

        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalTableTypeShort_Complex), ResourceType = typeof(DomainEnumTexts))]
        Complex = 3,

        [Description(Description = nameof(DomainEnumTexts.ProcedureEvalTableTypeShort_Preliminary), ResourceType = typeof(DomainEnumTexts))]
        Preliminary = 4,
    }
}
