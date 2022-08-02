using Eumis.Domain.Core;

namespace Eumis.Domain.Events
{
    public class ProjectVersionActivatedEvent : IDomainEvent
    {
        public int ProjectVersionId { get; set; }
    }
}
