using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class MessageSentEvent : IDomainEvent
    {
        public int MessageId { get; set; }
    }
}
