using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ProjectMACommunicationQuestionSentEvent : IDomainEvent
    {
        public int ProjectCommunicationId { get; set; }
    }
}
