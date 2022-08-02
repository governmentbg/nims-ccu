using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class MessageSentEvent : IDomainEvent
    {
        public int MessageId { get; set; }
    }
}
