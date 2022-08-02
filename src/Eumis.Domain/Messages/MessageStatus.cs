using Eumis.Common.Json;

namespace Eumis.Domain.Messages
{
    public enum MessageStatus
    {
        [Description(Description = nameof(DomainEnumTexts.MessageStatus_Draft), ResourceType = typeof(DomainEnumTexts))]
        Draft = 1,

        [Description(Description = nameof(DomainEnumTexts.MessageStatus_Sent), ResourceType = typeof(DomainEnumTexts))]
        Sent = 2,
    }
}
