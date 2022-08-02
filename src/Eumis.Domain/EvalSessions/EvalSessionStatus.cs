using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionStatus_Active), ResourceType = typeof(DomainEnumTexts))]
        Active = 2,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionStatus_Ended), ResourceType = typeof(DomainEnumTexts))]
        Ended = 3,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionStatus_Canceled), ResourceType = typeof(DomainEnumTexts))]
        Canceled = 4,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionStatus_EndedByLAG), ResourceType = typeof(DomainEnumTexts))]
        EndedByLAG = 5,
    }
}
