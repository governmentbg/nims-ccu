using Eumis.Common.Json;

namespace Eumis.Domain.EvalSessions
{
    public enum EvalSessionProjectWorkStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectWorkStatus_ComunicationInProgress), ResourceType = typeof(DomainEnumTexts))]
        ComunicationInProgress = 1,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionProjectWorkStatus_DraftVersion), ResourceType = typeof(DomainEnumTexts))]
        DraftVersion = 2,
    }
}