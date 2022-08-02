using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class AnswerReceivedEvent : IDomainEvent
    {
        public int ProjectCommunicationId { get; set; }
    }
}
