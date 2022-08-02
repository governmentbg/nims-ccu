using Eumis.Common.Json;

namespace Eumis.Domain.NonAggregates
{
    public enum EmailStatus
    {
        [Description(Description = nameof(DomainEnumTexts.EmailStatus_Pending), ResourceType = typeof(DomainEnumTexts))]
        Pending = 1,

        [Description(Description = nameof(DomainEnumTexts.EmailStatus_Sent), ResourceType = typeof(DomainEnumTexts))]
        Sent = 2,

        [Description(Description = nameof(DomainEnumTexts.EmailStatus_UknownError), ResourceType = typeof(DomainEnumTexts))]
        UknownError = 3,
    }
}