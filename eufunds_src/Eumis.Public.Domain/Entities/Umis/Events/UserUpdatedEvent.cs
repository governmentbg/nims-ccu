using Eumis.Public.Domain.Entities.Umis.Core;

namespace Eumis.Public.Domain.Entities.Umis.Events
{
    public class UserUpdatedEvent : IDomainEvent
    {
        public int UserId { get; set; }
    }
}
