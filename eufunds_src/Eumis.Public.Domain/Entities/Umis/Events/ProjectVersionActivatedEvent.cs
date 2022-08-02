using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class ProjectVersionActivatedEvent : IDomainEvent
    {
        public int ProjectVersionId { get; set; }
    }
}
