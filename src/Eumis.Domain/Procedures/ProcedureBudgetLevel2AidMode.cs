using Eumis.Common.Json;

namespace Eumis.Domain.Procedures
{
    public enum ProcedureBudgetLevel2AidMode
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedureBudgetLevel2AidMode_Deminimis), ResourceType = typeof(DomainEnumTexts))]
        Deminimis = 1,

        [Description(Description = nameof(DomainEnumTexts.ProcedureBudgetLevel2AidMode_StateAid), ResourceType = typeof(DomainEnumTexts))]
        StateAid = 2,

        [Description(Description = nameof(DomainEnumTexts.ProcedureBudgetLevel2AidMode_NotApplicable), ResourceType = typeof(DomainEnumTexts))]
        NotApplicable = 3,
    }
}
