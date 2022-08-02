using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class QuestionSentEvent : IDomainEvent
    {
        public int ProjectCommunicationId { get; set; }
    }
}
